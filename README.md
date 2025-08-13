# DungeonExplorer

간단한 조작으로 할 수 있는 **탐험 게임** 프로토타입입니다.  
튜토리얼 강의 기반이지만, 과제 요구에 맞춰 **UI 상태머신, 입력 처리, 장비 시스템, 아이템 시스템** 등을 응용·조합해 구현했습니다.

---

## ✨ 특징(Features)

- **Input System**: `Started / Performed / Canceled` 처리로 자연스러운 이동
- **타겟팅**: `Raycast + LayerMask` 기반 최근접 타겟 판정
- **아이템 시스템**: `ScriptableObject` 기반 아이템 정보 정의

---

## 🕹️ 조작법

- 이동: **WASD**
- 시점: **마우스 이동**
- 점프/앉기/달리기: **Space / LCtrl / LShift**
- 상호작용: **E**
- 인벤토리: **Tab**
- 메뉴/일시정지: **ESC**

---

## 🏗️ 프로젝트 구조
```
Assets/
├─ 01. Scenes/ (게임씬)
├─ 02. Scripts/
│ ├─ Player/ (플레이어 관련 스크립트)
│ ├─ Item/ (아이템 관련 스크립트)
│ ├─ Environment/ (환경 요소 스크립트)
│ └─ UI/ (UI 요소 제어)
├─ 03. Prefabs/ (재사용 가능한 오브젝트)
├─ 04. Animations/ (애니메이션)
└─ 09. Input/ (입력 시스템)
```

---

## 🔑 핵심 시스템

### 입력 처리(Input System)
- `Performed`에서 이동 벡터 갱신, `Canceled`에서 0으로 초기화 → 대각 이동/다중 입력 반영.

### 타겟팅
-  RayCast`로 타겟 탐색,
- `LayerMask`로 레벨 지형/목표 분리(시야 가림 대응).


### 아이템 시스템
- `ItemDataSO : ScriptableObject`로 이름/설명/효과 정의,

---

## 🧪 트러블슈팅

- `Started`만 사용 → 대각 이동 미반영 → **`Performed/Canceled` 병행**
- Animator 파라미터 대소문자(`IsMove` vs `isMove`) 불일치 → **이름 일관화**
- 레이어/태그 누락으로 타겟 판정 실패 → **LayerMask/CompareTag 재검토**
- 시간 대기 로직에 `while (Time.time < ...)` 사용 → **코루틴+`WaitForSeconds`로 교체**

---
