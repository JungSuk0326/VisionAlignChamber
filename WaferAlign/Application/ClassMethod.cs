using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class ClassMethod
    {
        public bool ConvertGray(Bitmap bm)
        {
            int ix, iy, cr, cg, cb, gray;
            Color c;
            for (ix = 0; ix < bm.Width; ix++)
            {
                for (iy = 0; iy < bm.Height; iy++)
                {
                    c = bm.GetPixel(ix, iy);
                    cr = c.R; cg = c.G; cb = c.B;
                    gray = (byte)(.299 * cr + .587 * cg + .114 * cb);
                    cr = cg = cb = gray;
                    bm.SetPixel(ix, iy, Color.FromArgb(cr, cg, cb));
                }
            }
            return true;
        }
        public bool ConvertNevative(Bitmap bm)
        {
            int ix, iy, ca, cr, cg, cb;
            Color c;
            for (ix = 0; ix < bm.Width; ix++)
            {
                for (iy = 0; iy < bm.Height; iy++)
                {
                    c = bm.GetPixel(ix, iy);
                    ca = c.A; cr = c.R; cg = c.G; cb = c.B;

                    cr = 255 - cr;
                    cg = 255 - cg;
                    cb = 255 - cb;
                    bm.SetPixel(ix, iy, Color.FromArgb(ca, cr, cg, cb));
                }
            }
            return true;
        }
        //======================================================================================
        // n개의 점을 이은 원

        public class Point
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        
        public (double centerX, double centerY, double radius) FitCircle(List<Point> points)
        //public double FitCircle(List<Point> points)
        {
            if (points == null || points.Count < 3)
                throw new ArgumentException("At least 3 points are required.");

            // 초기 추정값 설정 (평균 좌표 사용)
            double initialX = points.Average(p => p.X);
            double initialY = points.Average(p => p.Y);
            // 간단한 초기 반지름 추정치
            double initialRadius = points.Max(p => Math.Sqrt(Math.Pow(p.X - initialX, 2) + Math.Pow(p.Y - initialY, 2))) / 2;

            // 최적화 함수 정의
            // 매개변수 p = [centerX, centerY, radius]
            Func<Vector<double>, double> objectiveFunction = p =>
            {
                double cx = p[0];
                double cy = p[1];
                double r = p[2];
                double errorSum = 0;
                foreach (var point in points)
                {
                    // 각 점과 현재 추정된 원의 둘레까지의 거리 오차 제곱
                    double dist = Math.Sqrt(Math.Pow(point.X - cx, 2) + Math.Pow(point.Y - cy, 2));
                    double error = dist - r;
                    errorSum += error * error;
                }
                return errorSum;
            };
            return (0, 0, 0);
        }

        
        /*
            // 비선형 최적화 수행 (Broyden-Fletcher-Goldfarb-Shanno (BFGS) 알고리즘 사용)
            // 초기 추정값 벡터
            var initialGuess = Vector<double>.Build.Dense(new[] { initialX, initialY, initialRadius });

            // 주의: MathNet.Numerics의 최신 버전에 따라 사용법이 조금 다를 수 있습니다.
            // 이 예시는 일반적인 최적화 라이브러리 사용 패턴을 보여줍니다. 
            // 실제 구현 시 MathNet 문서의 NonLinearProgram 클래스를 참고해야 합니다.

            // 이 코드는 개념 설명용이며, MathNet.Numerics의 NonLinearProgram 클래스를 직접 사용해야 합니다.
            // 복잡한 설정이 필요하므로, 여기서는 라이브러리 없이 구현하는 다음 방법을 더 추천합니다.

            // ... (실제 최적화 코드 생략, 라이브러리 공식 문서 참조 필요) ...

            return (0, 0, 0); // Placeholder
            */
    }
}
    /*
    n개의 점으로 원 구하기
    public class Point
    {
        public double X, Y;
        public Point(double x, double y) { X = x; Y = y; }
    }

    public class MinimumEnclosingCircle
    {
        // 결과로 반환할 원 구조체
        public class Circle
        {
            public double X, Y, Radius;
            public Circle() { }
            public Circle(double x, double y, double r) { X = x; Y = y; Radius = r; }
        }

        // Welzl의 재귀 알고리즘 구현
        public static Circle MakeCircle(List<Point> points)
        {
            // 점 리스트를 섞어서 평균적인 성능을 보장
            var shuffled = points.OrderBy(a => Guid.NewGuid()).ToList();
            return MakeCircleRecursive(shuffled, 0, new List<Point>());
        }

        private static Circle MakeCircleRecursive(List<Point> points, int boundaryIndex, List<Point> boundary)
        {
            if (boundaryIndex == points.Count || boundary.Count == 3)
            {
                return MakeSmallestEnclosingCircle(boundary);
            }

            Point p = points[boundaryIndex];
            Circle circle = MakeCircleRecursive(points, boundaryIndex + 1, boundary);

            if (circle != null && IsInside(circle, p))
            {
                return circle;
            }

            // p가 현재 원 안에 포함되지 않으면 경계에 추가하고 다시 계산
            boundary.Add(p);
            circle = MakeCircleRecursive(points, boundaryIndex + 1, boundary);
            boundary.Remove(p); // 재귀 호출 후 원상 복구

            return circle;
        }

        // 경계점 1, 2, 3개로 원을 만드는 헬퍼 함수
        private static Circle MakeSmallestEnclosingCircle(List<Point> boundary)
        {
            switch (boundary.Count)
            {
                case 0: return null;
                case 1: return new Circle(boundary[0].X, boundary[0].Y, 0.0);
                case 2: return MakeCircleTwoPoints(boundary[0], boundary[1]);
                case 3: return MakeCircleThreePoints(boundary[0], boundary[1], boundary[2]);
            }
            return null;
        }

        private static bool IsInside(Circle c, Point p)
        {
            double dx = c.X - p.X;
            double dy = c.Y - p.Y;
            double dist = Math.Sqrt(dx * dx + dy * dy);
            // 부동 소수점 오차 감안하여 약간의 여유를 둠
            return dist <= c.Radius + 1e-9; 
        }

        // 두 점을 지름의 양 끝으로 하는 원 생성
        private static Circle MakeCircleTwoPoints(Point p1, Point p2)
        {
            double centerX = (p1.X + p2.X) / 2.0;
            double centerY = (p1.Y + p2.Y) / 2.0;
            double radius = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)) / 2.0;
            return new Circle(centerX, centerY, radius);
        }

        // 세 점을 지나는 원 생성 (이전 질문의 3점 원방정식 풀이)
        private static Circle MakeCircleThreePoints(Point p1, Point p2, Point p3)
        {
            // 생략: 세 점을 지나는 원의 중심과 반지름을 계산하는 수학적 공식 구현 필요
            // stackoverflow.com 참고
            // 이 부분은 기하학적 계산이 복잡하여 구현이 필요합니다.
        
            // 예시를 위한 임시 반환
            return new Circle(0, 0, 1); 
        }
    }
    */



