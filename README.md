# FirstJourney
First Original Game to Study Unity2D
# 2D 탑다운 액션 RPG
![image.png]([https://prod-files-secure.s3.us-west-2.amazonaws.com/12ada1a4-22d4-4d88-b1a5-af1565dce4b9/ab315160-c5fe-43b5-8662-4d0ba14f0121/image.png](https://github.com/minsforgh/FirstJourney/blob/main/docs/images/Overview.png))
## 소개
Unity 엔진으로 개발한 2D 탑다운 액션 RPG입니다. 컴포넌트 기반 설계와 ScriptableObject를 활용한 데이터 중심 아키텍처를 통해 확장성과 유지보수성을 높였습니다.

## 주요 기능
- WASD 이동 및 마우스 조준 전투 시스템
- 다양한 근접 및 원거리 무기
- 컴포넌트 기반 확장 가능한 적 AI
- 단계별 패턴을 갖는 보스 전투
- 상인과의 아이템 거래 시스템
- 인벤토리 및 무기 장착 시스템

## 기술 스택
- **개발 환경**: Unity
- **언어**: C#
- **설계 패턴**: 싱글톤, 컴포넌트, 팩토리, 옵저버 패턴
- **데이터 구조**: ScriptableObject 기반 데이터 아키텍처

## 핵심 시스템

### 1. 컴포넌트 기반 적 초기화 시스템
![적 초기화 시스템](enemy-init-core.png)

ScriptableObject와 컴포넌트 패턴을 활용한 유연한 적 초기화 시스템입니다. 코드 수정 없이 다양한 적 유형을 쉽게 생성하고 구성할 수 있습니다. 초기 설계의 코드 중복 문제를 해결하기 위해 모든 적의 속성을 ScriptableObject로 분리하고, 각 기능별 초기화기를 통해 필요한 컴포넌트만 동적으로 구성합니다.

### 2. 적 행동 패턴 시스템
![적 행동 패턴](![image.png](attachment:464b0d78-2e42-468f-b293-16962cb1eb4e:image.png))

상태 기반 AI와 다양한 컴포넌트를 통해 적의 행동 패턴(추적, 공격, 순찰)을 구현했습니다. 기본 추격과 장애물 회피 기능이 있는 향상된 추격 시스템, 그리고 근접 및 원거리 공격을 지원하는 확장 가능한 공격 시스템이 특징입니다.

### 3. 무기 시스템
![무기 시스템](weapon-system.png)

상속을 통한 확장성 있는 무기 구조를 설계했습니다. 기본 ItemData에서 WeaponData, MeleeWeaponData, RangedWeaponData로 세분화되며, 각 무기 타입은 고유한 공격 방식을 구현합니다. 특수 효과가 있는 무기(FlameEater)도 쉽게 추가할 수 있습니다.

### 4. 인벤토리 및 장비 시스템
![인벤토리 시스템](inventory-equipment.png)

InventorySystem과 WeaponManager를 싱글톤으로 구현하여 아이템과 무기를 관리합니다. 직관적인 UI 상호작용을 통해 아이템 사용 및 무기 교체가 가능하며, 무기 변경 시 관련 애니메이션도 자동으로 교체됩니다.

### 5. 보스 시스템
![보스 시스템](custom-boss-system.png)

단계별 특수 공격 패턴을 가진 보스 전투 시스템입니다. BossSpecialAttack 추상 클래스를 통해 공통 기능을 구현하고, 구체적인 보스별로 특화된 공격을 구현합니다. 체력에 따라 다른 패턴을 보이는 전투 시스템을 통해 긴장감을 높였습니다.
