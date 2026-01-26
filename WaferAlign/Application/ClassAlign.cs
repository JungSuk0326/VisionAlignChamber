
using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Imaging;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml.Schema;

namespace eMotion
{
    public class ClassAlign
    {
        const string PGM_VER = "1.0.0.0";
        // === 설정값 (튜닝 필요) ===
        const double DEFECT_MIN_DEPTH = 100.0;  // 노치 최소 깊이 (픽셀)
        const double DEFECT_MAX_WIDTH = 200.0;  // ★핵심: 이 값보다 넓으면 웨이퍼 곡선으로 간주하고 무시
        const double WAFER_RADIUS = 150.0;      // 웨이퍼 반지름
        const double ANGLE_STEP = 15.0;         // 이미지 간 촬영 각도
        const int NOISE_PIXEL = 10;             // 외각 노이즈 이미지 검색않함
        const double PER_PIXEL = 0.045;         // ★ 보정 필요 (임의의 값)

        public struct stSettingInfo
        {
            public double WaferRadius;     // unit mm
            public double AngleStep;       // unt deg
            public double PerPixel;        // unit um
            public int ShortCount;
        }
        stSettingInfo settingInfo;

        public struct stCenterInfo
        {
            public double TrueRadius;   // { get; set; }
            public double OffsetX;      // { get; set; }
            public double OffsetY;      // { get; set; }
            public double TotalOffset;  // { get; set; }
            public double DirectAngle;  // { get; set; }
        }
        stCenterInfo centerInfo;

        public struct stNotchInfo
        {
            public double Angle;
            public bool Found;
            public int Index;
            public double Size;
        }
        stNotchInfo notchInfo;

        public string PgmVer { get { return PGM_VER; } }
        public stSettingInfo SettingInfo { get{ return settingInfo; } }
        public stCenterInfo CenterInfo { get { return centerInfo; } }
        public stNotchInfo NotchInfo { get { return notchInfo; } }

        public List<Mat> listImg = new List<Mat>();
        //Mat sizeMat = null;
        Mat angleMat = null;
        Mat notchMat = null;
        //=====================================================================
        public int WidthMat() 
        {
            if (listImg.Count == 0) return 0;
            return listImg[0].Width; 
        }
        public int HeightMat()
        {
            if (listImg.Count == 0) return 0;
            return listImg[0].Height;
        }
   
        //public System.Drawing.Bitmap SizeImg() 
        //{
        //    if (sizeMat == null) return null;
        //    return MatToBitmap(sizeMat);
        //}
        
        public System.Drawing.Bitmap AngleImg() 
        {
            if (angleMat == null) return null;
            byte[] bytes = angleMat.ToBytes();

            System.Drawing.Bitmap bitmap = null;
            using (MemoryStream ms = new MemoryStream(bytes))
                bitmap = new System.Drawing.Bitmap(ms);
            return bitmap;
        }
        public System.Drawing.Bitmap NotchImg() 
        {
            if (notchMat == null) return null;
            byte[] bytes = notchMat.ToBytes();

            System.Drawing.Bitmap bitmap = null;
            using (MemoryStream ms = new MemoryStream(bytes))
                bitmap = new System.Drawing.Bitmap(ms);
            return bitmap;
        }

        //=====================================================================
        public ClassAlign()
        {
            settingInfo = new stSettingInfo();
            centerInfo = new stCenterInfo();
            notchInfo = new stNotchInfo();

            settingInfo.WaferRadius = 150.0;
            settingInfo.AngleStep = 15.0;
            //camera STC-GPB250BPL pixel size:2.5x2.5um, pixel count:5120x5120 => 12,800x12800um 
            //lens DTCM111-56-AL 0.329x => FOV: 38,905 x 38.905um 
            settingInfo.PerPixel = 7.59878;       
            settingInfo.ShortCount = (int)(360.0/ settingInfo.AngleStep);
        }
        public int SetCamera()
        {

            return 1;
        }
        public int SetCV(double waferRadius, double angleStep, double perPixel)
        {
            if (waferRadius > 250 && waferRadius < 350)
                settingInfo.WaferRadius = waferRadius;     // unit mm
            else return -1;
            if (angleStep > 10 && angleStep < 30)
                settingInfo.AngleStep = angleStep;       // unt deg
            else return -2;
            if (perPixel > 0 && perPixel < 1)
                settingInfo.PerPixel = perPixel;        // unit um
            else return -3;

            settingInfo.ShortCount = (int)(360.0 / angleStep);
            return 1;
        }
        public void SetThreshold(string sourceName, string binaryName)
        {
            var src = Cv2.ImRead(sourceName, ImreadModes.Grayscale);
            var binary = new Mat();
            Cv2.Threshold(src, binary, 128, 255, ThresholdTypes.Binary);
            binary.SaveImage(binaryName);
        }

        // gemini3 step2 wafer size
        // ==========================
        public int FindNotch()//string path)  //ok:1, no:0, error:-1
        {
            if (listImg.Count < 24) return -1;

            //string imageFolderPath = path; // @"C:\WaferImages"; // 이미지가 있는 폴더 경로
            //string outputFolderPath = Path.Combine(imageFolderPath, "Result");

            //if (!Directory.Exists(outputFolderPath))
            //    Directory.CreateDirectory(outputFolderPath);

            //// 파일명에서 숫자 추출하여 정렬 (001, 002 ... 024)
            //var files = Directory.GetFiles(imageFolderPath, "*.jpg")
            //                     .OrderBy(f => ExtractNumber(Path.GetFileName(f)))
            //                     .ToArray();

            //Console.WriteLine($"총 {files.Length}장의 이미지를 검사합니다.\n");

            notchInfo.Found = false;

            for (int i = 0; i < listImg.Count; i++)
            {
                // 노치 검사 실행
                if (ProcessImage(i))
                {
                    notchInfo.Index = i;
                    //if(!notchFound)
                    notchInfo.Found = true;
                }
            }

            return (notchInfo.Found) ? 1:0;
        }
        public bool ProcessImage(int Index) //, string outputFolder)
        {
            //using (Mat src = Cv2.ImRead(filePath))
            using (Mat gray = new Mat())
            using (Mat binary = new Mat())
            {
                if (listImg[Index].Empty()) return false;

                //int imgIndex = ExtractNumber(Path.GetFileName(filePath));

                // 1. 전처리
                Cv2.CvtColor(listImg[Index], gray, ColorConversionCodes.BGR2GRAY);

                // [중요] 배경이 흰색, 웨이퍼가 검은색이므로 BinaryInv 사용
                // 웨이퍼 영역이 흰색(255)이 되어야 윤곽선을 잡을 수 있음
                Cv2.Threshold(gray, binary, 100, 255, ThresholdTypes.BinaryInv);

                // 2. 윤곽선(Contours) 찾기
                Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(binary, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                // 가장 큰 윤곽선 추출 (웨이퍼 엣지)
                int maxIdx = -1;
                double maxArea = 0;
                for (int i = 0; i < contours.Length; i++)
                {
                    double area = Cv2.ContourArea(contours[i]);
                    if (area > maxArea) { maxArea = area; maxIdx = i; }
                }

                if (maxIdx == -1) return false;
                Point[] waferContour = contours[maxIdx];

                // 3. Convex Hull & Defects
                int[] hullIndices = Cv2.ConvexHullIndices(waferContour);

                // Hull 점이 너무 적으면 패스
                if (hullIndices.Length < 3) return false;

                Mat<Point> contourMat = Mat<Point>.FromArray(waferContour);
                Mat<int> hullMat = Mat<int>.FromArray(hullIndices);
                Mat<Vec4i> defects = new Mat<Vec4i>();

                Cv2.ConvexityDefects(contourMat, hullMat, defects);

                bool foundInThisImage = false;

                for (int i = 0; i < defects.Rows; i++)
                {
                    // Vec4i: [0]Start, [1]End, [2]Farthest, [3]Depth
                    Vec4i defect = defects.At<Vec4i>(i);

                    Point startPt = waferContour[defect.Item0];
                    Point endPt = waferContour[defect.Item1];
                    Point farPt = waferContour[defect.Item2];
                    double depth = defect.Item3 / 256.0; // OpenCV Depth 스케일 보정

                    // ★ 핵심 필터링 로직 ★
                    // 결함의 너비(Start와 End 사이의 거리) 계산
                    double defectWidth = startPt.DistanceTo(endPt);

                    // 1. 깊이가 너무 얕으면 노이즈 (Skip)
                    // 2. 너비가 너무 넓으면 웨이퍼 전체 곡선 (Skip)
                    // 3. 이미지 가장자리에 붙어있으면 짤린 면 (Skip)
                    if (depth > DEFECT_MIN_DEPTH) // && defectWidth < DEFECT_MAX_WIDTH) //674.92
                    {
                        //if (farPt.X > NOISE_PIXEL && farPt.X < src.Width - NOISE_PIXEL)
                        double imageCenterX = listImg[Index].Width / 2.0;
                        if (farPt.X > 10 && farPt.X < listImg[Index].Width - 10 &&
                        farPt.Y > 10 && farPt.Y < listImg[Index].Height - 10)
                        {
                            // --- 노치 발견 ---
                            foundInThisImage = true;

                            // === [정밀 각도 계산 로직] ===
                            /*
                            방법 1: 캘리브레이션(추천)
                                1. 웨이퍼 가장자리에 자(Ruler)를 대고 이미지를 한 장 찍습니다.
                                2. 이미지 상에서 10mm 눈금이 몇 픽셀인지 셉니다. (예: 10mm가 200픽셀이다.)
                                3. 10 / 200 = 0.05->MM_PER_PIXEL = 0.05 입력.
                            방법 2: 시야각(FOV) 정보 이용
                                1. 카메라가 가로로 보고 있는 실제 영역의 너비(FOV Width)를 안다면? (예: 가로 40mm 영역을 촬영 중)
                                2. 이미지 해상도 가로 픽셀을 확인합니다. (예: 1024 픽셀)
                                3. 40 / 1024 = 0.039->MM_PER_PIXEL = 0.039 입력.
                            참고: 
                                만약 카메라가 시계 방향으로 돌고 있다면 offsetDegrees를 더하는 것이 맞지만, 반대 방향이라면 finalAngle = baseAngle - offsetDegrees; 로 부호를 바꿔주어야 합니다.
                            */
                            // 1. 기준 각도 (Base Angle)
                            // 예: 1번 이미지=0도, 2번=15도... (1번 이미지가 0도 위치라고 가정 시)
                            double baseAngle = Index * ANGLE_STEP;

                            // 2. 픽셀 오차 (Pixel Offset)
                            // 이미지 중심보다 오른쪽에 있으면 +, 왼쪽에 있으면 - (카메라 방향에 따라 부호 반대일 수 있음)
                            double pixelOffset = farPt.X - imageCenterX;

                            // 3. 실제 물리적 거리 (Arc Length in mm)
                            double arcLengthMM = pixelOffset * PER_PIXEL;

                            // 4. 각도 보정값 (Offset Angle in Degree)
                            // 공식: theta = arc_length / radius
                            double offsetRadians = arcLengthMM / WAFER_RADIUS;
                            double offsetDegrees = offsetRadians * (180.0 / Math.PI);

                            // 5. 최종 각도
                            double finalAngle = baseAngle + offsetDegrees;

                            // 결과 출력
                            string log = $"[검출] IMG_{Index:D2} | Base: {baseAngle}° | Offset: {offsetDegrees:F2}° | Final: {finalAngle:F2}°";
                            Console.WriteLine(log);


                            // 시각화
                            angleMat = listImg[Index].Clone();
                            Cv2.Line(angleMat, new Point((int)imageCenterX, 0), new Point((int)imageCenterX, angleMat.Height), Scalar.Blue, 1); // 이미지 중심선
                            Cv2.Circle(angleMat, farPt, 5, Scalar.Red, -1);
                            Cv2.PutText(angleMat, $"{finalAngle:F2} deg", new Point(farPt.X - 30, farPt.Y + 40), HersheyFonts.HersheySimplex, 0.6, Scalar.Red, 2);
                            
                            Mat show = new Mat();
                            Cv2.Resize(angleMat, show, new Size(angleMat.Width / 5, angleMat.Height / 5));
                            Cv2.ImShow("Angle Image", show);
                            //string savePath = Path.Combine(outputFolder, $"Notch_{imgIndex:D2}_{finalAngle:F2}.jpg");
                            Cv2.ImWrite("d:\\angleMat.jpg", angleMat);

                            /*
                            // 각도 계산 (이미지 중심 기준)
                            double baseAngle = (imgIndex - 1) * ANGLE_STEP;

                            // 픽셀 오프셋을 이용한 미세 보정 (근사치)
                            // 이미지 중심에서 노치가 얼마나 떨어져 있는지 확인
                            double imageCenter = src.Width / 2.0;
                            double pixelOffset = farPt.X - imageCenter;

                            // *참고: 정확한 mm변환은 픽셀 분해능(mm/pixel)을 알아야 함.
                            // 여기서는 단순히 중심 기준 상대 위치만 표시

                            string logMsg = $"[검출] 파일:{Path.GetFileName(filePath)} | 각도(Base): {baseAngle}° | 깊이: {depth:F1}px | 위치(X): {farPt.X}";
                            Console.WriteLine(logMsg);

                            // 시각화
                            Cv2.Line(src, startPt, endPt, Scalar.Green, 2); // 노치 입구
                            Cv2.Circle(src, farPt, 5, Scalar.Red, -1);      // 노치 깊은점
                            Cv2.PutText(src, $"Notch {baseAngle}deg", new Point(farPt.X - 40, farPt.Y + 40),
                                HersheyFonts.HersheySimplex, 0.6, Scalar.Red, 2);
                            */
                        }
                    }
                }
                return foundInThisImage;
            }
        }

        //=====================================================================
        public int WaferSize() //string imageFolderPath )
        {
            if (listImg.Count < 24) return -1;
            // 파일명 숫자 기준 정렬
            //var files = Directory.GetFiles(imageFolderPath, "*.jpg")
            //                     .OrderBy(f => ExtractNumber(Path.GetFileName(f)))
            //                     .ToArray();

            //if (files.Length == 0) return;
            //Console.WriteLine($"[분석 시작] 총 {files.Length}장 분석 중...");


            // 데이터를 저장할 리스트 (각도, 측정된 반지름)
            List<Point2d> dataPoints = new List<Point2d>();

            //for (int i = 0; i < files.Length; i++)
            //{
            //    double angleDeg = i * ANGLE_STEP; // 0, 15, 30 ...
            //    double radius = GetRadiusFromImage(files[i]);

            //    dataPoints.Add(new Point2d(angleDeg, radius));
            //    // Console.WriteLine($"Angle {angleDeg,3:F0}° : {radius:F3} mm");
            //}
            double angleDeg, radius;
            bool ret = true;
            for (int i = 0; i < listImg.Count; i++)
            {
                angleDeg = i * ANGLE_STEP; // 0, 15, 30 ...
                radius = GetRadiusFromImage(i);
                if (radius < 0)
                    ret = false;
                if (ret == false) break;
                dataPoints.Add(new Point2d(angleDeg, radius));
            }

            // --- 중심 위치 계산 (핵심 로직) ---
            CalculateWaferCenter(dataPoints);
            Console.WriteLine(" [웨이퍼 중심 위치 분석 결과]");
            Console.WriteLine($" 1. 실제 웨이퍼 반지름 : {centerInfo.TrueRadius:F3} mm");
            Console.WriteLine($" 2. 웨이퍼 지름       : {centerInfo.TrueRadius * 2:F3} mm");
            Console.WriteLine($" 3. 중심 오차 (Offset)");
            Console.WriteLine($"    X 축 틀어짐 : {centerInfo.OffsetX:F3} mm");
            Console.WriteLine($"    Y 축 틀어짐 : {centerInfo.OffsetY:F3} mm");
            Console.WriteLine($" 4. 편심 정보");
            Console.WriteLine($"    총 거리 (Distance) : {centerInfo.TotalOffset:F3} mm");
            Console.WriteLine($"    방향 (Direction)   : {centerInfo.DirectAngle:F1} 도");
            Console.WriteLine("========================================");

            // (옵션) 시각화 이미지 저장 코드는 생략 (수치 데이터가 중요하므로)
            return ret? 1:0;
        }

        // --- 수학적 계산 함수 ---
        private void CalculateWaferCenter(List<Point2d> points)
        {
            // 1. 노치(Notch) 제거 필터링
            // 노치가 있는 부분은 반지름이 급격히 작아지므로 평균을 왜곡시킵니다.
            // 중위값(Median)과 비교하여 너무 작은 값은 계산에서 뺍니다.
            double medianR = points.Select(p => p.Y).OrderBy(v => v).ElementAt(points.Count / 2);
            var validPoints = points.Where(p => Math.Abs(p.Y - medianR) < 10.0).ToList(); // 차이가 2mm 미만인 것만 사용

            if (validPoints.Count < 12) Console.WriteLine("[경고] 유효한 데이터가 너무 적습니다. 노치 필터링 확인 필요.");

            // 2. 최소자승법 (Least Squares) or 푸리에 변환(DFT) 원리 이용
            // R(theta) = R_avg + X*cos(theta) + Y*sin(theta)
            // 이를 통해 X, Y 성분을 추출합니다.

            //double sumCos = 0, sumSin = 0;
            double sumRCos = 0, sumRSin = 0;
            double sumR = 0;
            int N = validPoints.Count;

            foreach (var p in validPoints)
            {
                double thetaRad = p.X * (Math.PI / 180.0); // Degree -> Radian
                double r = p.Y;

                sumR += r;
                sumRCos += r * Math.Cos(thetaRad);
                sumRSin += r * Math.Sin(thetaRad);
            }

            // 간단한 근사식 (데이터가 등간격일 때 매우 정확)
            // Offset X = (2/N) * Σ R * cos(θ)
            // Offset Y = (2/N) * Σ R * sin(θ)

            double offsetX = (2.0 / N) * sumRCos; // 주의: R이 이미 150 근처이므로 DC성분 제거 필요할 수 있음
            double offsetY = (2.0 / N) * sumRSin;

            // *정석적인 DC 성분(평균 반지름) 분리 방법:
            // R(t) ~= R_avg + x*cos + y*sin
            // 위 식은 R값이 0 근처일 때가 아니라 150 근처이므로,
            // 평균을 먼저 구하고 편차(Deviation)로 계산하는 것이 안전합니다.

            double avgRadius = sumR / N;
            double sumDevCos = 0;
            double sumDevSin = 0;

            foreach (var p in validPoints)
            {
                double thetaRad = p.X * (Math.PI / 180.0);
                double deviation = p.Y - avgRadius; // 평균에서의 차이

                sumDevCos += deviation * Math.Cos(thetaRad);
                sumDevSin += deviation * Math.Sin(thetaRad);
            }

            double finalX = (2.0 / N) * sumDevCos;
            double finalY = (2.0 / N) * sumDevSin;

            // X, Y 좌표계 방향에 따라 부호가 반대일 수 있습니다. (카메라 회전 방향 고려)
            // 여기서는 표준 수학 좌표계(반시계) 기준입니다.

            //return new WaferCenterInfo
            //{
            centerInfo.TrueRadius = avgRadius;
            centerInfo.OffsetX = finalX;
            centerInfo.OffsetY = finalY;
            centerInfo.TotalOffset = Math.Sqrt(finalX * finalX + finalY * finalY);
            centerInfo.DirectAngle = (Math.Atan2(finalY, finalX) * 180.0 / Math.PI + 360) % 360;
            //};
        }

        private double GetRadiusFromImage(int Index) //  string filePath)
        {
            //using (Mat src = Cv2.ImRead(filePath, ImreadModes.Grayscale))
            //{
            // 간단한 전처리
            Cv2.GaussianBlur(listImg[Index], listImg[Index], new Size(3, 3), 0);
            Mat edges = new Mat();
            Cv2.Canny(listImg[Index], edges, 50, 150);

            int centerX = listImg[Index].Width / 2;
            double centerY = listImg[Index].Height / 2.0;
            int detectedY = -1;

                // Center Column 스캔
                for (int y = 0; y < listImg[Index].Height; y++)
                {
                    if (edges.At<byte>(y, centerX) > 0)
                    {
                        detectedY = y;
                        break;
                    }
                }

            if (detectedY == -1) return -1; // WAFER_RADIUS; // 못 찾으면 기준값

                
                // 이미지 중심보다 위(y가 작음) -> 반지름 큼
                // 이미지 중심보다 아래(y가 큼) -> 반지름 작음
            double pixelOffset = centerY - detectedY;

            return WAFER_RADIUS + (pixelOffset * PER_PIXEL);
            //}
        }

        //=====================================================================
        //Notch size
        public int NotchSize(int Index) //string imagePath)
        {
            if (listImg.Count < Index) return -1;

            // 1. 이미지 로드 (원본 이미지 경로)
            //using (Mat src = Cv2.ImRead(imagePath))
            using (Mat gray = new Mat())
            using (Mat binary = new Mat())
            {
                if (listImg[Index].Empty())
                {
                    Console.WriteLine("이미지를 찾을 수 없습니다.");
                    return -1;
                }

                // 2. 전처리 (그레이스케일 -> 이진화)
                Cv2.CvtColor(listImg[Index], gray, ColorConversionCodes.BGR2GRAY);

                // 웨이퍼가 어두운 색, 배경이 밝은 색이므로 BinaryInv를 사용하여 
                // 웨이퍼 영역을 흰색(255)으로 만듭니다.
                Cv2.Threshold(gray, binary, 100, 255, ThresholdTypes.BinaryInv);

                // 3. 윤곽선 검출
                Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(binary, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                // 가장 큰 윤곽선(웨이퍼 전체) 찾기
                int maxIdx = -1;
                double maxArea = 0;
                for (int i = 0; i < contours.Length; i++)
                {
                    double a = Cv2.ContourArea(contours[i]);
                    if (a > maxArea)
                    {
                        maxArea = a;
                        maxIdx = i;
                    }
                }

                if (maxIdx == -1) return -1;
                Point[] waferContour = contours[maxIdx];

                // 4. Convex Hull 및 Defect 계산
                // Hull의 인덱스를 구해야 Defect를 계산할 수 있음
                int[] hullIndices = Cv2.ConvexHullIndices(waferContour);

                // 데이터 타입 변환 (OpenCvSharp 함수 요구사항)
                Mat<Point> contourMat = Mat<Point>.FromArray(waferContour);
                Mat<int> hullMat = Mat<int>.FromArray(hullIndices);
                Mat<Vec4i> defects = new Mat<Vec4i>();

                // ConvexityDefects 계산
                // 반환값 Vec4i: (Start_Index, End_Index, Farthest_Pt_Index, Fixpt_Depth)
                Cv2.ConvexityDefects(contourMat, hullMat, defects);

                // 5. 가장 큰 결함(노치) 찾기
                // 노치는 깊이가 가장 깊거나 면적이 가장 큰 Defect임
                double maxDefectArea = 0;
                List<Point> notchPolygon = new List<Point>();

                for (int i = 0; i < defects.Rows; i++)
                {
                    Vec4i def = defects.At<Vec4i>(i);
                    int startIdx = def.Item0;
                    int endIdx = def.Item1;
                    int farIdx = def.Item2;
                    double depth = def.Item3 / 256.0; // 픽셀 단위 깊이

                    // 노이즈 필터링 (너무 얕은 굴곡은 무시)
                    if (depth > 20)
                    {
                        // 해당 Defect 구간의 윤곽선 점들을 추출하여 다각형 구성
                        List<Point> currentPolygon = new List<Point>();

                        // Start Index에서 End Index까지의 점을 수집
                        // (배열 인덱스가 순환할 수 있으므로 처리 필요하지만, 보통 단일 객체는 순차적임)
                        if (endIdx > startIdx)
                        {
                            for (int k = startIdx; k <= endIdx; k++)
                                currentPolygon.Add(waferContour[k]);
                        }
                        else // 인덱스가 배열 끝을 넘어 앞쪽으로 이어질 때
                        {
                            for (int k = startIdx; k < waferContour.Length; k++)
                                currentPolygon.Add(waferContour[k]);
                            for (int k = 0; k <= endIdx; k++)
                                currentPolygon.Add(waferContour[k]);
                        }

                        // 추출한 다각형의 면적 계산
                        double currentArea = Cv2.ContourArea(currentPolygon);

                        // 가장 큰 면적을 가진 놈이 '노치'일 확률이 높음
                        if (currentArea > maxDefectArea)
                        {
                            maxDefectArea = currentArea;
                            notchPolygon = currentPolygon;
                        }
                    }
                }

                // 6. 결과 출력 및 시각화
                if (notchPolygon.Count > 0)
                {
                    // 픽셀 면적
                    double pixelArea = maxDefectArea;
                    // 실제 면적 (mm^2) = 픽셀면적 * (1px당 mm)^2
                    double realArea = pixelArea * (PER_PIXEL * PER_PIXEL);
                    notchInfo.Size = realArea;

                    Console.WriteLine($"노치 영역 픽셀 면적: {pixelArea:F2} px²");

                    // 만약 MM_PER_PIXEL을 설정했다면 아래 주석 해제
                    // Console.WriteLine($"실제 면적: {realArea:F3} mm²");
                    
                    // 시각화: 노란색으로 채우기
                    List<List<Point>> polysToDraw = new List<List<Point>> { notchPolygon };
                    notchMat = listImg[Index].Clone();

                    Cv2.FillPoly(notchMat, polysToDraw, Scalar.Yellow);

                    // 빨간선 긋기 (Start Point to End Point)
                    Cv2.Line(notchMat, notchPolygon.First(), notchPolygon.Last(), Scalar.Red, 2);

                    // 결과 저장 및 보기
                    Mat show = new Mat();
                    Cv2.Resize(notchMat, show, new Size(notchMat.Width / 5, notchMat.Height / 5));
                    Cv2.ImShow("Notch Area Image", show);

                    Cv2.ImWrite("d:\\notchAea.jpg", notchMat);
                    //Cv2.WaitKey(0);

                    return 1;
                }
                else
                {
                    Console.WriteLine("노치를 찾지 못했습니다.");
                    return 0;
                }
            }
        }


        //=====================================================================
        //Notch align 
        //프로그램 핵심 로직
        // 1. 전처리(Preprocessing) : 이미지를 흑백으로 변환하고 이진화(Thresholding)하여 경계선을 명확하게 만듭니다.
        // 2. 외곽선 검출 (Contour Detection): 흑백 경계면을 따라 점들의 집합(Contour) 을 찾습니다.
        // 3. 외곽선 분할 (Contour Splitting): 찾아낸 외곽선에서 '코너' 부분을 기준으로 위쪽 직선 구간과 아래쪽 직선 구간에 해당하는 점들을 분리합니다. 
        //            (이 예제에서는 코너가 가장 오른쪽에 있다는 특성을 이용해 분할합니다.)
        // 4. 직선 피팅(Line Fitting): 분리된 두 그룹의 점들에 대해 각각 가장 잘 맞는 직선 방정식(Cv2.FitLine)을 구합니다.
        // 5.교차점 계산 (Intersection Calculation): 구해진 두 직선 방정식을 연립하여 만나는 점(빨간색 점) 의 좌표를 계산합니다.
        public void Solve(string path)
        {
            Mat src = Cv2.ImRead(path);
            Mat gray = new Mat();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

            // 1. 이진화 (웨이퍼 검은색 -> 흰색으로 반전)
            Mat binary = new Mat();
            Cv2.Threshold(gray, binary, 120, 255, ThresholdTypes.BinaryInv);

            // 2. 외곽선 및 볼록 결함 찾기
            Cv2.FindContours(binary, out Point[][] contours, out _, RetrievalModes.External, ContourApproximationModes.ApproxNone);
            if (contours.Length == 0) return;
            Point[] contour = contours.OrderByDescending(c => c.Length).First();

            //int[] hull = Cv2.ConvexHullIndices(contour, true);
            //Mat defects = new Mat();
            //Cv2.ConvexityDefects(contour, hull, defects);
            int[] hull = Cv2.ConvexHullIndices(contour, true);
            int[] hullIndices = Cv2.ConvexHullIndices(contour, clockwise: true);
            Mat defects = new Mat();
            //Cv2.ConvexityDefects(contour, hull, defects);
            Cv2.ConvexityDefects(InputArray.Create(contour), InputArray.Create(hullIndices), defects);

            // 가장 깊은 결함(노치) 찾기
            double maxDepth = 0;
            Vec4i bestDefect = new Vec4i();
            for (int i = 0; i < defects.Rows; i++)
            {
                Vec4i d = defects.At<Vec4i>(i);
                if (d.Item3 / 256.0f > maxDepth) { maxDepth = d.Item3 / 256.0f; bestDefect = d; }
            }

            if (maxDepth > 5)
            {
                int startIdx = bestDefect.Item0; // 결함 시작 (어깨)
                int endIdx = bestDefect.Item1;   // 결함 끝 (어깨)
                int farIdx = bestDefect.Item2;   // 결함 정점 (가장 깊은 곳)

                // --- [핵심] 점 추출 구간 최적화 ---
                // 정점(farIdx)에서 어깨(start/end) 방향으로 30% ~ 80% 지점의 점들만 사용
                // 이렇게 하면 정점의 라운드(R) 영역을 완전히 배제할 수 있습니다.
                var leftPts = GetRefinedRange(contour, farIdx, startIdx, 0.3, 0.8);
                var rightPts = GetRefinedRange(contour, farIdx, endIdx, 0.3, 0.8);

                if (leftPts.Count > 3 && rightPts.Count > 3)
                {
                    Line2D lineL = Cv2.FitLine(leftPts, DistanceTypes.L2, 0, 0.01, 0.01);
                    Line2D lineR = Cv2.FitLine(rightPts, DistanceTypes.L2, 0, 0.01, 0.01);

                    // 수학적 교차점 계산
                    Point2d intersect = CalculateIntersection(lineL, lineR);

                    // 시각화 (선 연장해서 그리기)
                    DrawExtendedLine(src, lineL, Scalar.Lime, 150);
                    DrawExtendedLine(src, lineR, Scalar.Lime, 150);
                    Cv2.Circle(src, (Point)intersect, 5, Scalar.Red, -1);

                    Console.WriteLine($"교차점: ({intersect.X:F2}, {intersect.Y:F2})");
                }
            }

            Mat res = new Mat();
            Cv2.Resize(src, res, new Size(src.Width / 5, src.Height / 5));
            Cv2.ImShow("Result", res);
            Cv2.WaitKey();
        }

        // 정점에서 어깨 방향으로 특정 비율 구간의 점들만 추출하는 함수
        private List<Point2f> GetRefinedRange(Point[] contour, int fromIdx, int toIdx, double startRatio, double endRatio)
        {
            List<Point2f> pts = new List<Point2f>();
            int len = contour.Length;
            int totalDiff = (toIdx - fromIdx + len) % len;

            // 역방향(반시계) 처리 확인
            if (totalDiff > len / 2) totalDiff -= len;

            int startOffset = (int)(totalDiff * startRatio);
            int endOffset = (int)(totalDiff * endRatio);

            int count = Math.Abs(endOffset - startOffset);
            int step = (totalDiff > 0) ? 1 : -1;

            for (int i = 0; i <= count; i++)
            {
                int currIdx = (fromIdx + startOffset + (i * step) + len) % len;
                pts.Add(new Point2f(contour[currIdx].X, contour[currIdx].Y));
            }
            return pts;
        }

        //private Point2d CalculateIntersection(Line2D l1, Line2D l2)
        //{
        //    double det = l1.Vx * l2.Vy - l1.Vy * l2.Vx;
        //    if (Math.Abs(det) < 1e-6) 
        //        return new Point2d(0, 0);

        //    double t = ((l2.X1 - l1.X1) * l2.Vy - (l2.Y1 - l1.Y1) * l2.Vx) / det;
        //    return new Point2d(l1.X1 + t * l1.Vx, l1.Y1 + t * l1.Vy);
        //}

        private void DrawExtendedLine(Mat img, Line2D l, Scalar color, int len)
        {
            Cv2.Line(img,
                new Point(l.X1 - l.Vx * len, l.Y1 - l.Vy * len),
                new Point(l.X1 + l.Vx * len, l.Y1 + l.Vy * len),
                color, 2);
        }

        //2============================================================================
        public void DetectNotch(string path)
        {
            Mat src = Cv2.ImRead(path);
            Mat gray = new Mat();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

            // 1. 이진화: 014.jpg는 배경이 밝으므로 BinaryInv 사용
            Mat binary = new Mat();
            Cv2.Threshold(gray, binary, 150, 255, ThresholdTypes.BinaryInv);

            // 2. 외곽선 추출
            Cv2.FindContours(binary, out Point[][] contours, out _, RetrievalModes.External, ContourApproximationModes.ApproxNone);
            if (contours.Length == 0) return;
            Point[] contour = contours.OrderByDescending(c => c.Length).First();

            // 3. 볼록 결함(Defects)으로 노치 구간 찾기
            int[] hull = Cv2.ConvexHullIndices(contour, true);
            int[] hullIndices = Cv2.ConvexHullIndices(contour, clockwise: true);
            Mat defects = new Mat();
            //Cv2.ConvexityDefects(contour, hull, defects);
            Cv2.ConvexityDefects(InputArray.Create(contour), InputArray.Create(hullIndices), defects);

            double maxDepth = 0;
            Vec4i bestDefect = new Vec4i();

            for (int i = 0; i < defects.Rows; i++)
            {
                Vec4i d = defects.At<Vec4i>(i);
                float depth = d.Item3 / 256.0f;
                if (depth > maxDepth) { maxDepth = depth; bestDefect = d; }
            }

            if (maxDepth > 5)
            {
                // d.Item0: 시작점, d.Item1: 끝점, d.Item2: 가장 깊은 지점(정점)
                int startIdx = bestDefect.Item0;
                int endIdx = bestDefect.Item1;
                int farIdx = bestDefect.Item2;

                // 4. 왼쪽 사선과 오른쪽 사선 점들 추출 (정점 근처 곡선은 제외하고 샘플링)
                // 정점에서 양옆으로 10~40픽셀 떨어진 구간의 점들을 사용하여 직선 피팅
                var leftSidePoints = GetRangePoints(contour, startIdx + 5, farIdx - 10);
                var rightSidePoints = GetRangePoints(contour, farIdx + 10, endIdx - 5);

                if (leftSidePoints.Count > 5 && rightSidePoints.Count > 5)
                {
                    // 5. 각 사선을 직선으로 피팅
                    Line2D lineL = Cv2.FitLine(leftSidePoints, DistanceTypes.L2, 0, 0.01, 0.01);
                    Line2D lineR = Cv2.FitLine(rightSidePoints, DistanceTypes.L2, 0, 0.01, 0.01);

                    // 6. 두 직선의 수학적 교차점 계산
                    Point2d intersect = CalculateIntersection(lineL, lineR);

                    // 시각화
                    DrawFullLine(src, lineL, Scalar.Lime);
                    DrawFullLine(src, lineR, Scalar.Lime);
                    Cv2.Circle(src, (Point)intersect, 5, Scalar.Red, -1);

                    Console.WriteLine($"교차점 좌표: {intersect.X}, {intersect.Y}");
                }
            }

            Mat show = new Mat();
            Cv2.Resize(src, show, new Size(src.Width / 5, src.Height / 5));
            Cv2.ImShow("Result", show);
        }

        private List<Point2f> GetRangePoints(Point[] contour, int start, int end)
        {
            List<Point2f> pts = new List<Point2f>();
            int len = contour.Length;
            // 인덱스가 역전되는 경우(외곽선 연결부) 처리
            int count = (end - start + len) % len;
            for (int i = 0; i <= count; i++)
            {
                int idx = (start + i) % len;
                pts.Add(new Point2f(contour[idx].X, contour[idx].Y));
            }
            return pts;
        }

        private Point2d CalculateIntersection(Line2D l1, Line2D l2)
        {
            double det = l1.Vx * l2.Vy - l1.Vy * l2.Vx;
            if (Math.Abs(det) < 1e-6) return new Point2d(0, 0);
            double t = ((l2.X1 - l1.X1) * l2.Vy - (l2.Y1 - l1.Y1) * l2.Vx) / det;
            return new Point2d(l1.X1 + t * l1.Vx, l1.Y1 + t * l1.Vy);
        }

        private void DrawFullLine(Mat img, Line2D l, Scalar color)
        {
            double p1x = l.X1 - l.Vx * 100; 
            double p1y = l.Y1 - l.Vy * 100;
            double p2x = l.X1 + l.Vx * 100; 
            double p2y = l.Y1 + l.Vy * 100;
            Cv2.Line(img, (int)p1x, (int)p1y, (int)p2x, (int)p2y, color, 2);
        }

        //1================================================================================
        public void NotchSolve(string path)
        {
            Mat src = Cv2.ImRead(path);
            Mat gray = new Mat();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

            // 1. 이진화 (중요: BinaryInv를 사용하여 검은색 웨이퍼를 흰색으로 인식)
            Mat binary = new Mat();
            Cv2.Threshold(gray, binary, 100, 255, ThresholdTypes.BinaryInv);

            // 2. 외곽선 찾기
            Cv2.FindContours(binary, out Point[][] contours, out _, RetrievalModes.External, ContourApproximationModes.ApproxNone);
            if (contours.Length == 0) return;

            // 가장 큰 외곽선(웨이퍼) 선택
            Point[] waferContour = contours.OrderByDescending(c => c.Length).First();

            // 3. Convex Hull 및 Defects (파인 곳 찾기)
            int[] hull = Cv2.ConvexHullIndices(waferContour, true);
            int[] hullIndices = Cv2.ConvexHullIndices(waferContour, clockwise: true);
            Mat defects = new Mat();
            //Cv2.ConvexityDefects(waferContour, hull, defects);
            Cv2.ConvexityDefects(InputArray.Create(waferContour), InputArray.Create(hullIndices), defects);

            Point notchPoint = new Point();
            double maxDepth = 0;

            for (int i = 0; i < defects.Rows; i++)
            {
                Vec4i d = defects.At<Vec4i>(i);
                float depth = d.Item3 / 256.0f; // 파인 깊이

                // 가장 깊게 파인 곳을 노치로 간주 (노이즈 방지를 위해 depth > 5 조건)
                if (depth > maxDepth && depth > 5)
                {
                    maxDepth = depth;
                    notchPoint = waferContour[d.Item2]; // 가장 깊은 지점(Far Point)

                    // --- 녹색 선(가이드라인)을 그리기 위한 직선 피팅 ---
                    // 노치 지점 기준 좌우 30픽셀 정도의 점들을 모음
                    int farIdx = d.Item2;
                    var leftPts = GetPoints(waferContour, farIdx - 50, farIdx - 10);
                    var rightPts = GetPoints(waferContour, farIdx + 10, farIdx + 50);

                    if (leftPts.Count > 5 && rightPts.Count > 5)
                    {
                        Line2D lineL = Cv2.FitLine(leftPts, DistanceTypes.L2, 0, 0.01, 0.01);
                        Line2D lineR = Cv2.FitLine(rightPts, DistanceTypes.L2, 0, 0.01, 0.01);

                        DrawExtendedLine(src, lineL, Scalar.Lime);
                        DrawExtendedLine(src, lineR, Scalar.Lime);
                    }
                }
            }

            // 결과 표시
            if (maxDepth > 0)
            {
                Cv2.Circle(src, notchPoint, 10, Scalar.Red, -1); // 교차점(노치) 표시
                Console.WriteLine($"노치 검출 성공: {notchPoint}");
            }

            // 이미지 크기가 클 경우를 대비해 리사이즈 출력
            Mat show = new Mat();
            Cv2.Resize(src, show, new Size(src.Width / 5, src.Height / 5));
            Cv2.ImShow("Result", show);
        }

        // 외곽선 배열에서 안전하게 점들을 추출하는 헬퍼 함수
        private List<Point2f> GetPoints(Point[] contour, int start, int end)
        {
            List<Point2f> pts = new List<Point2f>();
            for (int i = start; i <= end; i++)
            {
                int idx = (i + contour.Length) % contour.Length;
                pts.Add(new Point2f(contour[idx].X, contour[idx].Y));
            }
            return pts;
        }

        private void DrawExtendedLine(Mat img, Line2D line, Scalar color)
        {
            float length = 200; // 선 길이
            Point p1 = new Point(line.X1 - line.Vx * length, line.Y1 - line.Vy * length);
            Point p2 = new Point(line.X1 + line.Vx * length, line.Y1 + line.Vy * length);
            Cv2.Line(img, p1, p2, color, 2);
        }
    }
}

 /* copilot ok
        //노치부분이 있는 이미지 찾기
        /* copilot
         300mm 웨이퍼 외각을 15도 간격으로 360도 회전하며 찍은 24장의 이미지인데 노치위치를 찾는 프로그램을 opencv를 이용한 c#으로 구현
         카메라 센터의 반지름이 150mm 이라고 할때 웨이퍼의 크기는?
         */
/*
public void NotchDetector(string path)
{
    string name = System.IO.Path.GetFileName(path);

    // 1. 이미지 불러오기
    Mat img = Cv2.ImRead(path, ImreadModes.Grayscale);

    // 2. 전처리
    Mat blurred = new Mat();
    Cv2.GaussianBlur(img, blurred, new Size(5, 5), 0);

    Mat edges = new Mat();
    Cv2.Canny(blurred, edges, 50, 150);

    // 3. 윤곽선 추출
    Cv2.FindContours(edges, out Point[][] contours, out HierarchyIndex[] hierarchy,
        RetrievalModes.External, ContourApproximationModes.ApproxSimple);

    // 가장 큰 컨투어 = 웨이퍼 외곽
    double maxArea = 0;
    Point[] waferContour = null;
    foreach (var contour in contours)
    {
        double area = Cv2.ContourArea(contour);
        if (area > maxArea)
        {
            maxArea = area;
            waferContour = contour;
        }
    }

    // 4. 웨이퍼 중심 계산
    Moments m = Cv2.Moments(waferContour);
    int cx = (int)(m.M10 / m.M00);
    int cy = (int)(m.M01 / m.M00);
    Point center = new Point(cx, cy);

    // 5. 노치 후보 찾기 (작은 돌출 영역)
    foreach (var contour in contours)
    {
        double area = Cv2.ContourArea(contour);
        if (area < 500 && area > 50) // 노치 크기 조건
        {
            Rect rect = Cv2.BoundingRect(contour);
            Cv2.Rectangle(img, rect, Scalar.Red, 2);

            // 각도 계산
            double angle = Math.Atan2(rect.Y - cy, rect.X - cx) * 180.0 / Math.PI;
            if (angle < 0) angle += 360;
            Console.WriteLine($"Notch candidate at angle: {angle} degrees");
        }
    }

    // 결과를 보고 어떤 depth 값이 노치인지 확인하세요.
    Mat resized = new Mat();
    Cv2.Resize(img, resized, new Size(), 0.2, 0.2, InterpolationFlags.Linear);

    Cv2.ImShow("Show", resized);
    Cv2.MoveWindow("Show", 600, 10);
}
*/ 
