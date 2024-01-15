# Text_RPG_Team
![GitHub](https://img.shields.io/badge/github-%23121011.svg?style=for-the-badge&logo=github&logoColor=white)
![Visual Studio](https://img.shields.io/badge/visual%20studio-9B4DE3?style=for-the-badge&logo=visual%20studio&logoColor=white)





 
## 개발 기간

### 멤버 구성

- **[팀장]신채윤** - 전투, 레벨업 기능 적용, 던전 스테이지 추가, 캐릭터 속도 시스템 추가, 몬스터 이름 중복 방지, 몬스터 스킬 추가 및 적용, 아이템 능력치 변경
- **[팀원]이인규** - MP 추가, 스킬 추가 및 적용, 콘솔 꾸미기
- **[팀원]김준서** - 직업 선택, 크리티컬, 회피기능추가, 몬스터 보상추가, 전투시 포션사용
- **[팀원]하지혜** : 캐릭터 생성, 레벨업 기능 추가,  상점 제작 및 인벤토리 아이템 적용, 포션 제작(HP, MP) 및 회복량 추가
  
<!--
주석표시방법
<img width = "10%" img alt="Static Badge" src="https://img.shields.io/badge/%ED%95%98%EC%A7%80%ED%98%9C%20-%2C?style=social&label=%ED%8C%80%EC%9B%90&color=%23640064">
https://shields.io/badges : 아이콘이나 명찰 등 만드는 곳
-->

### 주요 기능

#### 게임 시작 화면
![image](https://github.com/chai227chai/Text_RPG_Team/assets/154485025/59f75680-1031-4792-97d2-9f42bc7433f7)

----
#### 상태 보기
![image](https://github.com/chai227chai/Text_RPG_Team/assets/154485025/6ecf3729-62fe-4e17-ae2e-d836f2ff1489)

-
##### 1. 캐릭터 생성
![image](https://github.com/chai227chai/Text_RPG_Team/assets/154485025/54561e71-5b8d-40b2-9b5e-7a0998877bbb)

-

##### 2. 직업 선택
![image](https://github.com/chai227chai/Text_RPG_Team/assets/154485025/9ccb57c7-c970-4338-a3e4-2b4455d1d1a4)

-

----
#### 전투
-
##### 1. 스킬
-
##### 2. 치명타
-
##### 3. 회피
-
##### 4. 레벨 업
-

##### 5. 보상
-
----
#### 추가 구현
-
##### 1. 몬스터 종류 추가
-

##### 2. 회복 아이템 추가

    public enum PortionType
    {
        HP, MP
    }

    public enum PortionValue
    {
        Small = 30,
        Medium = 50, 
        Big = 100,
    }
    
- 회복 아이템 종류 및 회복량 변경.

##### 3. 아이템 착용
-

##### 4. 전투 스테이지 추가
-
##### 5. 보상
-
##### 6. 몬스터 스킬 추가

- ![image](https://github.com/chai227chai/Text_RPG_Team/assets/37549333/8fdad15d-6d13-4818-b39e-dd31f76423b1)
- 몬스터가 20% 확률로 스킬을 사용합니다. 특별한 몬스터는 더 자주 스킬을 사용합니다.

