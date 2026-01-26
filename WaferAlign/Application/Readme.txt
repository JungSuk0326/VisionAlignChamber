1. MultiamEx 비젼보드 클래스
	- void OpenBoard(uint driverIndex, string connector, string topology, string camfilePath) 
		카메라 정보를 전달하여 카메라 채널 열기 
	- void CloseBoard()
		카메라 채널 닫기
	- Bitmap GetImage()
		촬영이미지를 bitmap으로 반환
	- void SetAcquisition(eState index)
		카메라 모드변경 (IDLE, ACTIVE, READY)
	- void OnTrigger()
		이미지를 캡춰함
	- byte[] GetImagePointer()
		이미지 포인터 리턴
	- void GetWidth(out int value)
		이미지 폭
	- void GetHeight(out int value)
		이미지 높이
	- void SetExposureTime(int time)
		카메라 셔터오픈 시간을 입력
	- void SaveImage(string name)
		캡춰이미지를 파일로 저장

2. LfineLight 조명 컨트롤러 클래스
	- bool PortOpen(int value)
		통신포트를 열기 (true: 열기성공) 
	- void PortClose()
		통신포트를 닫기
	- void OnOff(bool value)
		조명 켜고 끄기
	- void Power(int value)
		조명 밝기 조절 (0~1023)

3. ClassAlign 비젼 프로세스
	- void AddImg(string path) 
		path 디렉토리 이미지를 가져옴
	- void AddItem(int width, int height, byte[] imageData)
		이미지의 폭과 높이, data를 지정하여 이미지를 가져옴
	- void DelItem()
		마지막 입력 이미지를 삭제
	- void ClearList()
		클래스의 이미지를 모두 삭제
	- ClassWafer.ResultInfo GetResult(bool flat = false)
		측정결과를 리턴
		bool TestEnd; 테스트 완료
        int Index1st; 노치: 노치가 있는 인덱스, 플렛: 플렛 경계가 있는 1번쨰 인덱스
        int Index2nd; 노치: 사용 X, 플렛: 플렛 경계가 있는 2번쨰 인덱스

        double OffAngle; 이미지 상의 각도
        double AbsAngle; 이미지 인덱스를 적용했을때 이미지
        double LangthX;  노치 : 노치 폭, 플렛: 플렛 길이
        double LangthY;  노치 : 노치 높이, 플렛: 센터와 플렛의 거리 

        WaferInfo Wafer ;웨이퍼의 중심점과 반지름
	- void TestMain(bool flat = false)
		측정 시작 : Notch(flat = false), Flat(flat = true)
	- void TestItem(string name, bool flat = false)
		테스트용으로 이미지 한장을 검사함
	- bool GetEnd(bool flat = false)
		측정이 끝났는지 확인
	- int CanvasX()
		이미지의 폭 리턴
	- int CanvasY() 
		이미지의 높이 리턴
	- void SaveImage(string folder)
		리스트의 모든이미지를 저장
	- void SetConfig(SettingInfo info, bool _default = false)
		테스트 정보를 지정
	- Bitmap GetImg(int index)
		지정한 인덱스의 이미지를 비트맵으로 리턴
	- Bitmap WaferImg(bool flat = false)
		웨이퍼의 결과이미지 리턴
	- Bitmap ResultImg(bool flat = false)
		노치 결과, 플렛 결과 이미지를 비트뱁으로 리턴

4. TurnTable 회전테이블 모션 클래스 [설비에선 사용 않함] 