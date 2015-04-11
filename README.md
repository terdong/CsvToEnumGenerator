# CsvToEnumGenerator
Csv 파일을 메모리에 로드 후 자동 생성된 Enum 값을 가지고 값을 가지고 오는 파서 및 리더기.

1. EnumFileGenerator.cs -
컴파일 된 .exe 파일을 실행하면,
GenerateAllEnumFile() 메소드 호출하면서, 해당 디렉토리 안에 있는 모든 csv 파일을 읽어서 enum으로 변환시켜줌.
"Local_Key.csv" 파일은 locale을 위해 따로 인덱싱을 함.

2. LocalizationManager.cs -
SetLocale(int index) 메소드는 해당 언어 리스트로 바꿈.
GetMessage(int index) 메소드는 해당 언어의 해당 인덱스에 해당하는 string 값을 반환함.

* Directory_Path 프로퍼티에 csv 파일이 위치한 디렉토리명을 지정해주면, 내부적으로 Initialize()메소드 호출함.
