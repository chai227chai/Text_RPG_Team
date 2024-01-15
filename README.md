# Text_RPG_Team
- 선택지를 입력하여 진행되는 Text RPG Game

![GitHub](https://img.shields.io/badge/github-%23121011.svg?style=for-the-badge&logo=github&logoColor=white)
![Visual Studio](https://img.shields.io/badge/visual%20studio-9B4DE3?style=for-the-badge&logo=visual%20studio&logoColor=white)
<!--
![표시할이름](https://img.shields.io/badge/표시할이름-색상?style=for-the-badge&logo=기술스택아이콘&logoColor=white)
-->
<!--
주석표시방법
<img width = "10%" img alt="Static Badge" src="https://img.shields.io/badge/%ED%95%98%EC%A7%80%ED%98%9C%20-%2C?style=social&label=%ED%8C%80%EC%9B%90&color=%23640064">
https://shields.io/badges : 아이콘이나 명찰 등 만드는 곳
-->

 ----
## 개발 기간
#### **2024/01/09 ~ 2024/01/16**
----
## 멤버 구성

- **[팀장]신채윤** - 전투, 레벨업 기능 적용, 던전 스테이지 추가, 캐릭터 속도 시스템 추가, 몬스터 이름 중복 방지, 몬스터 스킬 추가 및 적용, 아이템 능력치 변경
- **[팀원]이인규** - MP 추가, 스킬 추가 및 적용, 콘솔 꾸미기
- **[팀원]김준서** - 직업 선택, 크리티컬, 회피기능추가, 몬스터 보상추가, 전투시 포션사용
- **[팀원]하지혜** : 캐릭터 생성, 레벨업 기능 추가,  상점 제작 및 인벤토리 아이템 적용, 포션 제작(HP, MP) 및 회복량 추가
 
----
## 주요 기능 목차
<!--
이동 링크 넣는 법
[화면에 보일 텍스트](#이동할 곳의 제목)
띄어쓰기 있을 경우 -를 적어줘야 적용 가능
-->

[1. 게임 시작화면](#게임-시작화면)

[2. 상태보기](#상태보기) 

[3. 전투](#전투)

[4. 추가 구현](#추가-구현)

----
### 게임 시작화면

![image](https://github.com/chai227chai/Text_RPG_Team/assets/154485025/59f75680-1031-4792-97d2-9f42bc7433f7)

![image](https://github.com/chai227chai/Text_RPG_Team/assets/37549333/bbefc77d-9ad7-4e5d-b6c2-bddbb1255a13)

![image](https://github.com/chai227chai/Text_RPG_Team/assets/154485025/2f8e6013-bc64-4247-9366-699820dcbd25)

----
### 상태보기
![image](https://github.com/chai227chai/Text_RPG_Team/assets/154485025/fbaf9815-7952-427d-ae3d-3e0d4ae1aab6)

- 설정한 캐릭터의 이름과 선택한 직업을 볼 수 있습니다.
- 착용한 아이템이 있다면 아이템의 능력치가 추가되어 나타납니다.
-
#### 1. 캐릭터 생성
![image](https://github.com/chai227chai/Text_RPG_Team/assets/154485025/54561e71-5b8d-40b2-9b5e-7a0998877bbb)

- 원하는 캐릭터의 이름을 작성합니다.

#### 2. 직업 선택
![image](https://github.com/chai227chai/Text_RPG_Team/assets/154485025/38e9bf7b-c0ac-4334-9a1f-240123cd5f5c)

![image](https://github.com/chai227chai/Text_RPG_Team/assets/154485025/9c7036d9-c0a9-451a-8eab-1d1fe9790de9)

- 직업을 선택할 수 있으며 직업에 따라 능력치와 사용할 수 있는 스킬이 다릅니다.

----
### 전투
- 기본 공격과 스킬을 이용하여 던전에 있는 몬스터를 잡아서 스테이지 클리어를 목표로 합니다.
- 클리어할 때마다 스테이지가 증가하며 전투 난이도도 올라갑니다.
- 
#### 1. 스킬
예시)전사

![image](https://github.com/chai227chai/Text_RPG_Team/assets/154485025/bb719fef-a7cb-4300-8da1-6d7a68d90e7a)

- 직업당 3개의 스킬을 보유하고 있습니다.
- 스킬마다 공격받는 적의 수와 공격 데미지가 다릅니다.
- 공격 데미지는 현재 공격력의 영향을 받습니다.
- 스킬을 사용할 때마다 일정 MP를 소모합니다.
- 
#### 2. 치명타
-
#### 3. 회피
-
#### 4. 레벨 업
- 쓰러트린 몬스터의 레벨1 당 경험치 1을 얻습니다.
- 레벨마다 정해진 경험치를 얻으면 레벨 업을 하게 됩니다.
- 레벨 업을 하게 되면 공격력과 방어력이 소량 증가하게 됩니다.

#### 5. 보상
- 쓰러트린 몬스터마다 한개의 보상을 드랍합니다.
- 보상에는 착용가능한 장비와 포션이 있습니다.

----
### 추가 구현
-


#### 1. 회복 아이템 추가

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
    
- HP와 MP 회복 물약이 있습니다. 각 30, 50, 100씩 회복이 가능한 물약이 존재합니다.
- 처음 캐릭터 생성 시 HP 30, MP 30 회복 물약을 3개씩 가지고 있습니다.

#### 2. 아이템 착용
```
public enum ItemType
{
    WEAPON, ARMOR, SHOES
}
```
![image](https://github.com/chai227chai/Text_RPG_Team/assets/154485025/ac3e6ca7-450a-4a22-887d-d1b3ccee9f82)


- 상점에서 구매하거나 던전에서 얻은 아이템이 인벤토리에 들어가게 됩니다.
- 인벤토리에서 아이템을 착용할 수 있습니다.
- 아이템의 종류마다 한개의 아이템만 착용 가능합니다.
- 같은 종류가 착용되어 있으면 착용된 아이템은 해제되고 선택한 아이템이 착용됩니다.
- 착용한 아이템을 선택하면 착용 해제됩니다.
- 착용한 아이템의 스탯은 적용됩니다.

#### 3. 전투 스테이지 추가
- 클리어할 때마다 한 층씩 증가합니다.
- 클리어할 때마다 등장하는 몬스터의 수가 증가합니다.
- 
#### 4. 몬스터 종류 추가
- 스테이지에 맞춰 다양한 몬스터들이 등장합니다.
- 일정 층에서는 보스가 등장합니다.

- 
#### 5. 몬스터 스킬 추가

![image](https://github.com/chai227chai/Text_RPG_Team/assets/37549333/8fdad15d-6d13-4818-b39e-dd31f76423b1)
- 몬스터가 20% 확률로 스킬을 사용합니다. 특별한 몬스터는 더 자주 스킬을 사용합니다.


#### 6. 게임 저장 및 불러오기

 ![image](https://github.com/chai227chai/Text_RPG_Team/assets/37549333/5cf4f02d-d8b4-433a-8a2f-6afb7fd01f8c)

 ![image](https://github.com/chai227chai/Text_RPG_Team/assets/37549333/f5b33ee8-da17-4cac-8055-d43a6ddc9049)
 
 ![image](https://github.com/chai227chai/Text_RPG_Team/assets/37549333/aa7ab7c8-362d-40f6-87f3-5b608b9a32c2)

 - 게임 저장 시 현재 시간으로 세이브 파일이 생성됩니다.
 - 게임 불러오기를 선택할 시, 원하는 파일을 선택하여 불러올 수 있습니다.
