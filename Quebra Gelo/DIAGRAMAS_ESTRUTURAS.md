# 📐 Diagramas e Estruturas - Quebra Gelo

## 1. ARQUITETURA DO PROJETO

```
┌─────────────────────────────────────────────────────┐
│              QUEBRA GELO - ARQUITETURA               │
├─────────────────────────────────────────────────────┤
│                                                       │
│  ┌──────────────────────────────────────────────┐  │
│  │         GameController (Singleton)            │  │
│  │  - Controla estado geral do jogo              │  │
│  │  - Gerencia turnos (Player 1/2)               │  │
│  │  - Detecta vitória/derrota                    │  │
│  └──────────────┬───────────────────────────────┘  │
│                 │                                    │
│     ┌───────────┴───────────┐                       │
│     ▼                       ▼                        │
│  ┌─────────────┐      ┌──────────────┐              │
│  │BoardManager │      │  IceBlock    │              │
│  │ - Física    │      │ - Dano (0-3) │              │
│  │ - Gravidade │      │ - Sprites    │              │
│  │ - Conexão   │      │ - OnMouseDown│              │
│  └─────────────┘      └──────────────┘              │
│                                                       │
│     ┌────────────────────────┐                       │
│     ▼                        ▼                        │
│  ┌─────────┐          ┌──────────────┐              │
│  │ Penguin │          │     UI       │              │
│  │- Queda  │          │- Game Over   │              │
│  │- Colisão│          │- Reiniciar   │              │
│  └─────────┘          └──────────────┘              │
│                                                       │
└─────────────────────────────────────────────────────┘
```

---

## 2. ESTRUTURA DA CENA UNITY

```
Hierarquia (Scene Tree):
├── Main Camera
│   └── (Orthographic, Size: 5)
│
├── Board (Container - vazio)
│   ├── IceBlock_1 → IceBlock.cs
│   ├── IceBlock_2 → IceBlock.cs
│   ├── IceBlock_3 → IceBlock.cs
│   ├── IceBlock_4 → IceBlock.cs
│   ├── IceBlock_5 → IceBlock.cs
│   ├── IceBlock_6 → IceBlock.cs
│   ├── IceBlock_7 → IceBlock.cs
│   ├── IceBlock_8 → IceBlock.cs
│   ├── IceBlock_9 → IceBlock.cs
│   └── IceBlock_10 → IceBlock.cs
│
├── Penguin → Penguin.cs
│   └── (CircleCollider2D, Rigidbody2D)
│
├── Borders (Container - vazio)
│   ├── Border_Top → (BoxCollider2D)
│   ├── Border_Bottom → (BoxCollider2D)
│   ├── Border_Left → (BoxCollider2D)
│   └── Border_Right → (BoxCollider2D)
│
├── GameManager → GameController.cs + BoardManager.cs
│   └── (Singleton - UI references)
│
└── Canvas (UI)
    └── GameOverPanel (Hidden)
        ├── WinnerText → (Text UI)
        └── RestartButton → (Button)
```

---

## 3. FLUXO DE CLIQUE EM UM BLOCO

```
        JOGADOR CLICA EM BLOCO
                 │
                 ▼
         OnMouseDown() disparado
         (Unity raycast automático)
                 │
                 ▼
     GameController.HandleBlockClick(block)
                 │
    ┌────────────┴────────────┐
    ▼                         ▼
block.Hit()         BoardManager.CheckStability()
    │                         │
    ├─ hitCount++             ├─ Obtém todos os blocos
    │                         │
    ├─ hitCount < 3?          ├─ Para cada bloco:
    │  ├─ Sim: Muda sprite      │
    │  └─ Não: Destroy()        ├─ IsConnectedToBorder()?
    │                           │  ├─ Sim: Bloco seguro ✓
    ▼                           │  └─ Não: Rigidbody2D
 Retorna                        │      (cai) ✗
                                ▼
                         Troca turno
                    currentPlayer = 1 ou 2
```

---

## 4. VISUALIZAÇÃO DO TABULEIRO

```
     Y
     ▲
   2 │ ┌─ ┌─ ┌─ ┌─ ┌─
     │ │B1│B2│B3│B4│B5│
   1 │ ├─ ├─ ├P ├─ ├─
     │ │B6│B7│  │B9│B10│
   0 │ ├─ ├─ └─ ├─ ├─
     │ ┌─────────────────
  -1 │ ├─ ├─ ├─ ├─ ├─
     │ │ │ │ │ │ │
  -2 ├─┼─┼─┼─┼─┼─┼─→ X
     │
    -3  -2  -1   0   1   2   3

Legenda:
B# = Bloco de Gelo
P  = Pinguim (centro)
┌─┐ = Bloco
─  = Borda

GridSize = 1.1 (espaçamento entre blocos)
```

---

## 5. MÁQUINA DE ESTADOS - BLOCO

```
┌─────────────────────────────────────────┐
│        ESTADOS DO BLOCO DE GELO         │
└─────────────────────────────────────────┘

  ┌─────────────┐
  │   INTACTO   │
  │ hitCount:0  │
  │ Sprite: [0] │
  └──────┬──────┘
         │ Hit() chamado
         ▼
  ┌─────────────┐
  │ DANO LEVE   │
  │ hitCount:1  │
  │ Sprite: [1] │
  └──────┬──────┘
         │ Hit() chamado
         ▼
  ┌─────────────┐
  │DANO MÉDIO   │
  │ hitCount:2  │
  │ Sprite: [2] │
  └──────┬──────┘
         │ Hit() chamado (3º clique)
         ▼
  ┌─────────────┐
  │ DESTRUÍDO   │
  │ hitCount:3+ │
  │Destroy()    │
  └─────────────┘
```

---

## 6. MÁQUINA DE ESTADOS - JOGO

```
┌───────────────────────────────────────────────┐
│      ESTADOS DO JOGO GERAL                     │
└───────────────────────────────────────────────┘

  ┌──────────────┐
  │   JOGANDO    │
  │isGameOver:F  │
  │Current: 1/2  │
  └──────┬───────┘
         │
  ┌──────┴──────┐
  ▼             ▼
Clique+        Pinguim cai
Troca turno    (Y < -5f)
  │             │
  └──────┬──────┘
         ▼
  ┌──────────────┐
  │   GAME OVER  │
  │isGameOver:T  │
  │ Vencedor: 1/2│
  └──────┬───────┘
         │
         ▼ Clica RESTART
  ┌──────────────┐
  │ REINICIANDO  │
  │LoadScene()   │
  └──────┬───────┘
         │
         ▼
  ┌──────────────┐
  │   JOGANDO    │ (volta ao início)
  └──────────────┘
```

---

## 7. DETECÇÃO DE CONECTIVIDADE (BFS)

```
Entrada: Bloco em posição (0, 0)

┌─────────────────────────────────────┐
│   Bloco precisa estar conectado      │
│   a uma BORDA para ser seguro        │
└─────────────────────────────────────┘

Exemplo: Bloco isolado

Inicial:
┌─ ┌─ ┌─
│1 │2 │3 │        ← Conectados à borda (seguros)
├─ ├─ ├─
│4 │X │5 │        ← X isolado!
├─ ├─ ├─
│6 │7 │8 │        ← Conectados à borda (seguros)

BFS Search:
1. Comeca em (0, 0) - posição de X
2. Procura neighbors (up, down, left, right)
   - Encontra bloco 2 (up) ✓
3. Procura neighbors de 2
   - Encontra bloco 3 (right) ✓
   - Encontra bloco 1 (left) ✓
4. Continua até alcançar uma borda
   - Se encontra borda → SAFE ✓
   - Se não encontra (isolado) → FALL ✗

Queue:
(0,0) → (0,1) → (1,1) → ... → BORDER encontrado!

SE isolado:
(0,0) → (0,1) → ... → NENHUMA BORDA
→ Rigidbody2D.AddComponent() → CAIR
```

---

## 8. LAYERS E COLISÕES

```
Physics2D Layer Matrix:
┌────────────────────────────────────────┐
│              LAYERS                     │
├────────────────────────────────────────┤
│                                         │
│  IceLayer (8)                          │
│  ├─ BoxCollider2D (Is Trigger: false)  │
│  ├─ Detectado por Physics2D.OverlapPoint
│  └─ Cai quando isolado                 │
│                                         │
│  BorderLayer (9)                       │
│  ├─ BoxCollider2D (Is Trigger: false)  │
│  ├─ Sinaliza blocos SEGUROS            │
│  └─ 4 bordas (top, bottom, left, right)│
│                                         │
│  PenguinLayer (10)                     │
│  ├─ CircleCollider2D (Is Trigger: F)   │
│  ├─ Rigidbody2D (Dynamic)              │
│  └─ Detecta queda Y < -5f              │
│                                         │
└────────────────────────────────────────┘
```

---

## 9. SEQUÊNCIA TEMPORAL DE UM TURNO

```
TEMPO: 0ms
┌─────────────────────────────────┐
│ Jogador 1 clica em IceBlock_5    │
└─────────────────────────────────┘
          │
          ▼ (1ms)
┌─────────────────────────────────┐
│ OnMouseDown() dispara            │
│ GameController.HandleBlockClick()│
└─────────────────────────────────┘
          │
          ├─────────────┬──────────────┐
          ▼ (5ms)       ▼ (5ms)        ▼ (10ms)
┌──────────────┐  ┌───────────────┐  ┌─────────────┐
│ block.Hit()  │  │CheckStability │  │Troca turno  │
│ hitCount: 1  │  │Para cada bloco│  │currentPlayer│
│Muda sprite   │  │IsConnected?   │  │= 2          │
└──────────────┘  │Se não → Queda │  └─────────────┘
                  └───────────────┘
          │
          ▼ (15ms)
┌─────────────────────────────────┐
│ Debug.Log("Turno jogador 2")     │
└─────────────────────────────────┘
          │
          ▼ (16ms)
┌─────────────────────────────────┐
│ Aguardando próximo clique        │
│ Jogador 2 pode clicar            │
└─────────────────────────────────┘
```

---

## 10. ESTRUTURA DE SPRITES DO BLOCO

```
damageSprites Array:

[0] = Intacto          [1] = Dano Leve
┌─────────────┐       ┌─────────────┐
│   ░░░░░░░   │       │  ░░ ░░░░░   │
│   ░░░░░░░   │       │  ░░░░ ░░░   │
│   ░░░░░░░   │       │  ░░░░░░░░   │
│   ░░░░░░░   │       │  ░ ░░░░░░   │
│   ░░░░░░░   │       │  ░░░░░░░░   │
└─────────────┘       └─────────────┘


[2] = Dano Médio       [3] = Quebrado
┌─────────────┐       ┌─────────────┐
│  ░ ░ ░░░░   │       │  Não usado  │
│  ░░░ ░ ░░░  │       │  (Bloco é   │
│   ░ ░░░ ░   │       │   destruído)│
│  ░░ ░ ░░░░  │       │             │
│  ░ ░░░░  ░  │       │             │
└─────────────┘       └─────────────┘

Legend: ░ = Gelo, Espaço vazio = Quebrado
```

---

## 11. FLUXO COMPLETO DA VITÓRIA

```
CENÁRIO: Jogador 2 derruba Jogador 1

1. Vários blocos foram clicados e removidos
   
2. Pinguim fica pendurado por apenas 1 bloco

3. Jogador 2 clica no último bloco

4. IceBlock.Hit():
   ├─ hitCount = 3
   └─ Destroy(gameObject)

5. BoardManager.CheckStability():
   ├─ Nenhum bloco sob o Pinguim!
   └─ Todos os blocos caem (mas já foram destruídos)

6. Penguin.Update():
   ├─ transform.position.y < -5f? SIM!
   └─ GameController.TriggerGameOver(currentPlayer)
       └─ currentPlayer = 2 (era a vez de Jogador 2)

7. GameController.TriggerGameOver(2):
   ├─ isGameOver = true
   ├─ winner = 1 (o outro jogador)
   ├─ gameOverPanel.SetActive(true)
   └─ winnerText.text = "Jogador 1 venceu! O pinguim caiu."

8. UI Exibe:
   ┌─────────────────────────────────┐
   │ Jogador 1 venceu! O pinguim caiu│
   │                                 │
   │         [REINICIAR]             │
   └─────────────────────────────────┘

9. Jogador clica REINICIAR:
   └─ SceneManager.LoadScene()
      └─ Cena reinicia do zero
```

---

## 12. GRID DE POSIÇÕES RECOMENDADO (2x5)

```
Coordenadas (X, Y, Z):

Linha 1 (Y = 1.0):
(-2, 1, 0) | (-1, 1, 0) | (0, 1, 0) | (1, 1, 0) | (2, 1, 0)
    B1    |     B2      |    B3    |    B4    |    B5

Linha 2 (Y = -0.1):
(-2,-0.1,0)| (-1,-0.1,0)| (0,-0.1,0)| (1,-0.1,0)| (2,-0.1,0)
    B6     |     B7      |    B8    |    B9    |   B10

Pinguim: (0, 0.5, 0) - Centro superior

Espaçamento: 1.0 unidade (gridSize = 1.1 para pequeno overlap)
```

---

## 13. REFERÊNCIAS NO INSPECTOR

```
GameController Component:
├─ Current Player: 1
├─ Is Game Over: false
├─ Game Over Panel: (UI Canvas → GameOverPanel)
└─ Winner Text: (Canvas → GameOverPanel → WinnerText)

BoardManager Component:
├─ Ice Layer: IceLayer (8)
├─ Border Layer: BorderLayer (9)
└─ Grid Size: 1.1

IceBlock Component:
├─ damageSprites[0]: (Intacto)
├─ damageSprites[1]: (Leve)
├─ damageSprites[2]: (Médio)
└─ damageSprites[3]: (Quebrado - opcional)
```

---

## 14. CHECKLIST DE COMPONENTS

```
┌─ IceBlock (Prefab)
│  ├─ ✓ Transform
│  ├─ ✓ Sprite Renderer (Sprite atribuído)
│  ├─ ✓ Box Collider 2D (Is Trigger: false)
│  └─ ✓ IceBlock.cs (damageSprites[] preenchido)
│
├─ Penguin
│  ├─ ✓ Transform (Posição: centro)
│  ├─ ✓ Sprite Renderer
│  ├─ ✓ Circle Collider 2D
│  ├─ ✓ Rigidbody 2D (Dynamic, Gravity: 1)
│  └─ ✓ Penguin.cs
│
├─ Borders (x4)
│  ├─ ✓ Transform (Posição: bordas)
│  ├─ ✓ Box Collider 2D (Is Trigger: false)
│  └─ ✓ Layer: BorderLayer
│
└─ GameManager
   ├─ ✓ GameController.cs
   ├─ ✓ BoardManager.cs
   ├─ References preenchidas
   └─ ✓ No GameObject raiz
```

