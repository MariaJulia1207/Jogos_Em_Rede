# 🔧 REFERÊNCIA VISUAL - Componentes no Inspector

## 📱 COMO USAR ESTE DOCUMENTO

Cada seção mostra EXATAMENTE como deve aparecer no Inspector do Unity.

---

## 🐧 1. PENGUIN - CONFIGURAÇÃO COMPLETA

### 1.1 Transform
```
Transform
├─ Position
│  ├─ X: 0
│  ├─ Y: 0.5
│  └─ Z: 0
├─ Rotation
│  ├─ X: 0
│  ├─ Y: 0
│  └─ Z: 0
└─ Scale
   ├─ X: 1
   ├─ Y: 1
   └─ Z: 1
```

### 1.2 Sprite Renderer
```
Sprite Renderer
├─ Sprite: (seu sprite de pinguim)
├─ Color: 
│  ├─ R: 255
│  ├─ G: 255
│  ├─ B: 255
│  └─ A: 255 (opaco)
├─ Flip X: ☐
├─ Flip Y: ☐
└─ Sorting Order: 1
```

### 1.3 Circle Collider 2D
```
Circle Collider 2D
├─ Is Trigger: ☐ (unchecked)
├─ Used by Effector: ☐
├─ Offset
│  ├─ X: 0
│  └─ Y: 0
└─ Radius: 0.5
```

### 1.4 Rigidbody 2D ⚠️ IMPORTANTE
```
Rigidbody 2D
├─ Body Type: Dynamic          ← DEVE SER DYNAMIC!
├─ Gravity Scale: 1            ← DEVE SER 1!
├─ Mass: 1
├─ Linear Drag: 0
├─ Angular Drag: 0
├─ Gravity Scale: 1
├─ Collision Detection: Continuous
├─ Constraints
│  ├─ Freeze Position
│  │  ├─ X: ☐
│  │  ├─ Y: ☐
│  │  └─ Z: ☐
│  └─ Freeze Rotation
│     ├─ X: ☐
│     ├─ Y: ☐
│     └─ Z: ☑ (CHECKED!)  ← IMPORTANTE!
└─ Auto Simulation: ☑
```

### 1.5 Penguin Script
```
Penguin (Script)
└─ (Nenhuma propriedade pública)
```

### 1.6 Layer (lado superior direito)
```
Layer: PenguinLayer  ← IMPORTANTE!
```

---

## ❄️ 2. ICEBLOCK PREFAB - CONFIGURAÇÃO

### 2.1 Transform
```
Transform
├─ Position: (X: 0, Y: 0, Z: 0)  ← Será mudado ao instanciar
├─ Rotation: (0, 0, 0)
└─ Scale: (1, 1, 1)
```

### 2.2 Sprite Renderer
```
Sprite Renderer
├─ Sprite: (sprite de gelo/quadrado)
├─ Color
│  ├─ R: 135
│  ├─ G: 206  (Azul claro #87CEEB)
│  ├─ B: 235
│  └─ A: 255
└─ Sorting Order: 0
```

### 2.3 Box Collider 2D
```
Box Collider 2D
├─ Is Trigger: ☐ (unchecked)
├─ Auto Tiling: ☑
├─ Offset: (X: 0, Y: 0)
└─ Size: (X: 1, Y: 1)
```

### 2.4 IceBlock Script ⚠️ IMPORTANTE
```
IceBlock (Script)
├─ Damage Sprites: Array Size 4
│  ├─ Element 0: [Sprite Intacto]      ← Arraste sprite
│  ├─ Element 1: [Sprite Dano Leve]    ← Arraste sprite
│  ├─ Element 2: [Sprite Dano Médio]   ← Arraste sprite
│  └─ Element 3: [Sprite Dano Pesado]  ← Arraste sprite
```

### 2.5 Layer
```
Layer: IceLayer  ← IMPORTANTE!
```

---

## 🎮 3. GAMEMANAGER - GAME CONTROLLER

### 3.1 Transform
```
Transform
├─ Position: (0, 0, 0)
├─ Rotation: (0, 0, 0)
└─ Scale: (1, 1, 1)
```

### 3.2 Game Controller Script ⚠️ REFERÊNCIAS IMPORTANTES
```
Game Controller (Script)
├─ Current Player: 1           ← readonly
├─ Is Game Over: false         ← readonly
├─ Game Over Panel: [GameOverPanel]  ← ARRASTE DA UI!
├─ Winner Text: [WinnerText]         ← ARRASTE DA UI!
└─ (Nenhum getter/setter público)
```

**COMO PREENCHER:**
```
1. Selecione GameManager
2. Inspector → Game Controller
3. Game Over Panel → clique no círculo
4. Selecione "GameOverPanel" da Canvas
5. Winner Text → clique no círculo  
6. Selecione "WinnerText" dentro de GameOverPanel
```

---

## 🏗️ 4. GAMEMANAGER - BOARD MANAGER

### 4.1 Transform
```
Transform
├─ Position: (0, 0, 0)
├─ Rotation: (0, 0, 0)
└─ Scale: (1, 1, 1)
```

### 4.2 Board Manager Script ⚠️ LAYERS IMPORTANTES
```
Board Manager (Script)
├─ Ice Layer: IceLayer      ← Selecione IceLayer (8)
├─ Border Layer: BorderLayer ← Selecione BorderLayer (9)
└─ Grid Size: 1.1           ← Deixe assim
```

**COMO PREENCHER LAYERS:**
```
1. Inspector → Board Manager
2. Ice Layer → Dropdown
3. Selecione "IceLayer"
4. Border Layer → Dropdown
5. Selecione "BorderLayer"
```

---

## 🚧 5. BORDER - QUALQUER UM (TOP, BOTTOM, LEFT, RIGHT)

### Exemplo: Border_Top

### 5.1 Transform
```
Transform
├─ Position: (X: 0, Y: 2, Z: 0)     ← Varia por borda!
├─ Rotation: (0, 0, 0)
└─ Scale: (1, 1, 1)
```

### 5.2 Box Collider 2D
```
Box Collider 2D
├─ Is Trigger: ☐ (IMPORTANTE!)
├─ Auto Tiling: ☑
├─ Offset: (X: 0, Y: 0)
└─ Size: (X: 6, Y: 0.5)  ← Varia por borda!
```

**Tamanhos por Borda:**
```
Border_Top:    Size (6, 0.5),   Position (0, 2, 0)
Border_Bottom: Size (6, 0.5),   Position (0, -1.5, 0)
Border_Left:   Size (0.5, 3),   Position (-3, 0.5, 0)
Border_Right:  Size (0.5, 3),   Position (3, 0.5, 0)
```

### 5.3 Layer
```
Layer: BorderLayer  ← TODAS as 4 bordas!
```

---

## 🎨 6. CANVAS - UI SETUP

### 6.1 Canvas (Raiz)
```
Canvas
├─ Render Mode: Screen Space - Overlay
├─ Scale Factor: 1
├─ Reference Resolution: (1920, 1080)
└─ Match: Match Width or Height
```

### 6.2 Rect Transform
```
Rect Transform (Canvas)
├─ Anchor Preset: Full Rectangle
├─ Anchors: Min (0, 0) Max (1, 1)
├─ Pivot: (0.5, 0.5)
└─ Offsets: All 0
```

---

## 🎪 7. GAMEOVER PANEL

### 7.1 Transform
```
Name: GameOverPanel
Active: ☐ (UNCHECKED - importante!)
```

### 7.2 Rect Transform
```
Rect Transform
├─ Anchor Preset: Center
├─ Position: (X: 0, Y: 0, Z: 0)
├─ Width: 600
├─ Height: 400
├─ Pivot: (0.5, 0.5)
└─ Rotation: (0, 0, 0)
```

### 7.3 Image
```
Image
├─ Source Image: (none) ou branco
├─ Color: 
│  ├─ R: 0
│  ├─ G: 0
│  ├─ B: 0
│  └─ A: 204 (alpha ~80%)
├─ Image Type: Simple
└─ Preserve Aspect: ☐
```

### 7.4 Layout Group (opcional)
```
Vertical Layout Group (opcional)
├─ Child Force Expand: Width ☑, Height ☑
└─ Child Control Size: Width ☑, Height ☑
```

---

## 💬 8. WINNER TEXT (dentro de GameOverPanel)

### 8.1 Rect Transform
```
Name: WinnerText
Rect Transform
├─ Position: (X: 0, Y: 150, Z: 0)
├─ Width: 500
├─ Height: 100
├─ Pivot: (0.5, 1)
└─ Alignment: Center Top
```

### 8.2 Text
```
Text (Legacy)
├─ Text: "Jogador X venceu! O pinguim caiu."
├─ Font: Arial ou default
├─ Font Size: 40
├─ Font Style: Bold (opcional)
├─ Alignment: Center
└─ Color: (R: 255, G: 255, B: 255, A: 255)
```

### 8.3 Layout Element (opcional)
```
Layout Element
├─ Preferred Width: 500
├─ Preferred Height: 100
└─ Layout Priority: 1
```

---

## 🔘 9. RESTART BUTTON (dentro de GameOverPanel)

### 9.1 Rect Transform
```
Name: RestartButton
Rect Transform
├─ Position: (X: 0, Y: -100, Z: 0)
├─ Width: 200
├─ Height: 60
├─ Pivot: (0.5, 0.5)
└─ Alignment: Center
```

### 9.2 Image (Background)
```
Image
├─ Source Image: (branco/default)
├─ Color: (R: 100, G: 150, B: 255, A: 255)  Azul
└─ Image Type: Simple
```

### 9.3 Button
```
Button
├─ Interactable: ☑
├─ On Click (): [+]          ← Clique aqui!
│  ├─ Object: GameManager    ← Arraste GameObject
│  └─ Function: GameController → RestartGame()
└─ Navigation: Automatic
```

**COMO CONECTAR ON CLICK:**
```
1. Selecione RestartButton
2. Inspector → Button Component
3. On Click () → Clique [+]
4. Arraste GameManager para "Object"
5. Dropdown → GameController → RestartGame()
6. Verifique que aparece: "GameController.RestartGame()"
```

### 9.4 Text (Label dentro do Button)
```
Text (Legacy)
├─ Text: "REINICIAR"
├─ Font Size: 30
├─ Font Style: Bold
├─ Alignment: Center
└─ Color: Branco (255, 255, 255, 255)
```

### 9.5 Button Colors (Visual Feedback)
```
Button
└─ Colors
   ├─ Normal Color: (100, 150, 255)     Azul médio
   ├─ Highlighted Color: (150, 180, 255) Azul claro
   ├─ Pressed Color: (50, 100, 200)     Azul escuro
   ├─ Selected Color: (100, 150, 255)   Azul médio
   ├─ Disabled Color: (200, 200, 200)   Cinza
   └─ Color Multiplier: 1
```

---

## 📊 10. CÂMERA (MAIN CAMERA)

### 10.1 Transform
```
Transform
├─ Position: (X: 0, Y: 0, Z: -10)  ← IMPORTANTE!
├─ Rotation: (0, 0, 0)
└─ Scale: (1, 1, 1)
```

### 10.2 Camera ⚠️ IMPORTANTE
```
Camera
├─ Projection: Orthographic         ← NÃO perspective!
├─ Size: 5                          ← IMPORTANTE!
├─ Near Clip Plane: 0.3
├─ Far Clip Plane: 1000
├─ Field of View: (cinza, ortho)
└─ Viewport Rect: (0, 0, 1, 1)
```

### 10.3 Background
```
Camera
├─ Background Type: Solid Color
└─ Background Color:
   ├─ R: 30
   ├─ G: 58
   ├─ B: 90  (Azul escuro)
   └─ A: 255
```

---

## 🎯 CHECKLIST DE PREENCHIMENTO

```
PENGUIN:
☐ Position: (0, 0.5, 0)
☐ Rigidbody2D Body Type: Dynamic
☐ Rigidbody2D Gravity Scale: 1
☐ Rigidbody2D Freeze Rotation Z: ☑
☐ Layer: PenguinLayer

ICEBLOCK PREFAB:
☐ Damage Sprites: Array Size 4 (preenchido)
☐ Box Collider 2D Size: (1, 1)
☐ Layer: IceLayer
☐ Salvo em Assets/Prefabs/

GAMEMANAGER:
☐ GameController.Game Over Panel: preenchido
☐ GameController.Winner Text: preenchido
☐ BoardManager.Ice Layer: IceLayer
☐ BoardManager.Border Layer: BorderLayer
☐ BoardManager.Grid Size: 1.1

BORDAS (4):
☐ Todas com BoxCollider2D Is Trigger: ☐
☐ Todas com Layer: BorderLayer
☐ Posições corretas (top, bottom, left, right)

CANVAS:
☐ GameOverPanel Active: ☐ (unchecked)
☐ WinnerText dentro de GameOverPanel
☐ RestartButton com On Click() conectado
☐ Button apontando para GameController.RestartGame()

CAMERA:
☐ Position: (0, 0, -10)
☐ Projection: Orthographic
☐ Size: 5
```

---

## 🔍 COMO ENCONTRAR REFERÊNCIAS RAPIDAMENTE

### Para preencher "Game Over Panel":
```
1. Ctrl+F (ou Cmd+F no Mac)
2. Digite "GameOverPanel"
3. Clique na Canvas
4. Vê? Arraste para o field!
```

### Para preencher "Winner Text":
```
1. Expand GameOverPanel na hierarquia
2. Vê "WinnerText" dentro?
3. Arraste para o field!
```

### Para preencher "OnClick()":
```
1. Selecione RestartButton
2. Inspector → Button → On Click ()
3. [+] Adicionar listener
4. Arraste "GameManager" no primeiro campo
5. Dropdown: GameController → RestartGame()
```

---

## ❌ ERROS COMUNS NO INSPECTOR

```
ERRO: "NullReferenceException"
→ Verificar se campos de referência estão vazios
→ Arrastrar objetos corretamente

ERRO: "Object reference not set to an instance"
→ GameController.instance é null
→ Verificar se GameController.Awake() está rodando

ERRO: "No collider found"
→ GameObject não tem Collider2D
→ Adicionar BoxCollider2D ou CircleCollider2D

ERRO: "Object is not on layer XYZ"
→ Layer errado selecionado
→ Verifique Layer dropdown

AVISO: "RigidBody2D missing"
→ Bloco caindo mas sem Rigidbody2D
→ CheckStability() não está rodando
```

---

## 📱 LAYOUT RECOMENDADO NA TELA

```
┌─────────────────────────────────────────────────┐
│ Game View                                       │
├─────────────────────────────────────────────────┤
│                                                 │
│          Scene (vê tabuleiro aqui)              │
│          ┌──────────────────────┐               │
│          │  Bordas              │               │
│          │  ┌────────────────┐   │               │
│          │  │ B1  B2  B3     │   │               │
│          │  │ B4  P   B5     │   │               │
│          │  │ B6  B7  B8     │   │               │
│          │  └────────────────┘   │               │
│          │                        │               │
│          └──────────────────────┘               │
│                                                 │
│          Inspector (lado)                       │
│          (vê componentes aqui)                  │
│                                                 │
└─────────────────────────────────────────────────┘
```

---

**Use este documento como referência visual ao configurar cada componente!**

Imprime se necessário ou mantenha aberto enquanto trabalha. 📖

