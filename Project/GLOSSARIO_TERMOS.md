# 📚 GLOSSÁRIO - Quebra Gelo

## A

### Anchor
Ponto de referência para UI Elements (Canvas)
- **Exemplo:** Botão ancorado ao topo-direita
- **Relacionado:** UI Offset

### Awake()
Função Unity chamada quando GameObject é criado/instanciado
- **Uso:** Inicializar referências
- **Ordem:** Antes de Start()
- **Projeto:** Penguin.Awake(), IceBlock.Awake()

---

## B

### BFS (Breadth-First Search)
Algoritmo de busca em largura - explora vizinhos em camadas
- **Uso:** Verificar conectividade de blocos à borda
- **Implementação:** BoardManager.IsConnectedToBorder()
- **Estruturas:** Queue (fila), HashSet (visitados)

### Body Type (Rigidbody2D)
Tipo de corpo físico:
- **Dynamic:** Afetado por gravidade (pinguim, blocos caindo)
- **Kinematic:** Controlado por script
- **Static:** Imóvel (bordas)
- **Projeto:** Pinguim = Dynamic, Bordas = Static

### Border
Limite do tabuleiro - detecta blocos conexos
- **Layer:** BorderLayer
- **Função:** Marcar blocos seguros
- **Quantidade:** 4 (top, bottom, left, right)

### Box Collider 2D
Colisor retangular para detecção
- **Is Trigger = false:** Física real (blocos)
- **Is Trigger = true:** Apenas detecção (não recomendado aqui)
- **Projeto:** Blocos e bordas usam false

---

## C

### Canvas
Container de UI no Unity
- **Render Mode:** Screen Space (aparece na tela)
- **Contém:** Painéis, botões, textos
- **Projeto:** Contém GameOverPanel

### Circle Collider 2D
Colisor circular para detecção
- **Radius:** Tamanho do círculo
- **Projeto:** Pinguim usa para colisão com blocos

### Collider 2D
Componente que define forma física do GameObject
- **Tipos:** BoxCollider2D, CircleCollider2D, PolygonCollider2D
- **Função:** Colisão e detecção de cliques
- **Projeto:** Blocos (Box), Pinguim (Circle)

### Component
Parte de um GameObject que adiciona funcionalidade
- **Exemplos:** Transform, Sprite Renderer, Collider, Script
- **Conceito:** Composição > Herança (em Unity)

---

## D

### Damage Progression
Sistema progressivo de dano
- **Níveis:** 0 (intacto), 1 (leve), 2 (médio), 3 (destruído)
- **Sprites:** 4 imagens mostrando deterioração
- **Projeto:** IceBlock.damageSprites[]

### Destroy()
Remove GameObject da cena
- **Projeto:** IceBlock destruído após 3 cliques
- **Efeito:** Física afetada (pinguim perde suporte)

### Debug.Log()
Imprime mensagens no Console
- **Uso:** Acompanhar lógica, detectar erros
- **Projeto:** Turnos, queda de blocos, game over

### Drag
Fricção do movimento (Rigidbody2D)
- **0 = Sem fricção:** Movimento livre
- **Maior = Mais fricção:** Desacelera
- **Projeto:** Pinguim tem Drag = 0 (sem fricção)

---

## E

### Event
Ação que dispara callback (on-click, colisão)
- **Projeto:** Button.On Click() → RestartGame()

### FindObjectOfType<T>()
Busca GameObject com script específico
- **Uso:** Localizar GameController ou BoardManager
- **Performance:** Evitar em Update(), usar cache
- **Projeto:** GameController.HandleBlockClick()

---

## F

### Frame
Renderização única
- **Hertz (Hz):** 60 FPS = 60 frames por segundo
- **Update():** Executado 1x por frame

### FindObjectsOfType<T>()
Retorna array de todos GameObjects com script
- **Uso:** IceBlock[] allBlocks = FindObjectsOfType<IceBlock>()
- **Projeto:** BoardManager.CheckStability()

### Freeze Rotation
Bloqueia rotação em eixo específico
- **Projeto:** Pinguim com "Freeze Rotation Z"
- **Motivo:** Impede pinguim girar durante queda

---

## G

### Gravity
Aceleração para baixo (física)
- **Padrão:** (0, -9.81) m/s²
- **Escala:** Gravity Scale multiplica este valor
- **Projeto:** Pinguim com Gravity Scale = 1

### Game Over
Término da partida
- **Acionada:** Quando pinguim cai (Y < -5)
- **Efeito:** Mostra painel, desativa controles

### Gameobject
Entidade básica da Unity
- **Composto:** Transform + Componentes
- **Projeto:** Pinguim, Blocos, Bordas, GameManager

### Grid
Sistema de posicionamento em linha/coluna
- **Tamanho:** gridSize = 1.1
- **Formato:** 2 linhas × 5 colunas = 10 blocos

---

## H

### HashSet<T>
Estrutura de dados para conjunto único
- **Uso:** Rastrear posições visitadas em BFS
- **Benefício:** Operação O(1) de busca
- **Projeto:** visited.Add(neighbor) evita loops

---

## I

### IceBlock
Script do bloco de gelo
- **Responsabilidades:**
  - Gerenciar dano (hitCount)
  - Mudar sprite conforme dano
  - Destruir quando necessário
  - Detectar cliques (OnMouseDown)

### Inspector
Painel de configuração de GameObjects e Componentes
- **Uso:** Arrastrar referências, mudar valores
- **Projeto:** Configurar damageSprites, layers, etc

### Is Trigger
Propriedade de Collider
- **false:** Colisão física real (impede passagem)
- **true:** Apenas detecção (sem física)
- **Projeto:** Blocos/bordas = false (para física)

---

## L

### Layer
Categoria de GameObject para filtragem
- **Uso:** Physics, rendering, raycast seletivo
- **Projeto:** IceLayer (8), BorderLayer (9), PenguinLayer (10)

### LayerMask
Máscara para selecionar camadas
- **Uso:** Physics2D.OverlapPoint(pos, layerMask)
- **Projeto:** iceLayer, borderLayer no BoardManager

---

## M

### Monobehaviour
Classe base de todos scripts em Unity
- **Métodos:** Awake(), Start(), Update()
- **Lifecycle:** Inicialização → Loop → Destruição

---

## O

### OnMouseDown()
Função chamada quando clica em GameObject
- **Requer:** Collider (não-trigger)
- **Funcionamento:** Unity faz raycast automaticamente
- **Projeto:** IceBlock.OnMouseDown() → HandleBlockClick()

### Orthographic (Câmera)
Modo de câmera 2D (sem perspectiva)
- **Alternativa:** Perspective (3D)
- **Projeto:** Main Camera em Orthographic

### OverlapPoint()
Detecta colisores em posição específica
- **Uso:** Verificar se existe bloco/borda numa posição
- **Projeto:** Physics2D.OverlapPoint(neighbor, iceLayer)

---

## P

### Penguin
Script do pinguim
- **Responsabilidades:**
  - Detectar queda (Y < -5)
  - Acionar GameController.TriggerGameOver()

### Physics 2D
Sistema de física 2D do Unity
- **Componentes:** Rigidbody2D, Collider2D
- **Simulação:** Gravidade, colisões, velocidade

### Prefab
Template reutilizável de GameObject
- **Uso:** Criar múltiplas cópias idênticas
- **Projeto:** IceBlock.prefab para 10 instâncias

### Project Settings
Configurações globais do projeto
- **Acesso:** Edit → Project Settings
- **Uso:** Layers, Physics, Quality

---

## Q

### Queue<T>
Estrutura de dados FIFO (First In, First Out)
- **Método:** Enqueue() adiciona, Dequeue() remove
- **Uso:** BFS em BoardManager
- **Projeto:** Queue de posições a explorar

---

## R

### Raycast
Linha invisível para detectar colisores
- **Uso:** OnMouseDown() faz raycast automaticamente
- **Alternativa:** Physics2D.Raycast() manual

### Renderer
Componente que desenha visual (sprite, mesh)
- **Projeto:** SpriteRenderer para blocos/pinguim

### Rigidbody 2D
Componente de física para objetos dinâmicos
- **Propriedades:** Mass, Gravity Scale, Drag, Velocity
- **Projeto:** Pinguim (Dynamic), Blocos caindo (adicionado dinamicamente)

### RestartGame()
Método que recarrega cena
- **Implementação:** SceneManager.LoadScene()
- **Acionada:** Botão RESTART

---

## S

### Scene
Arquivo .unity contendo tudo do jogo
- **Arquivos:** QuebraGelo.unity
- **Conteúdo:** GameObjects, assets, configurações

### SceneManager
Gerenciador de cenas
- **Método:** LoadScene() recarrega cena
- **Uso:** Restart, mudança de nível

### Singleton
Padrão de design - apenas 1 instância
- **Implementação:** public static GameController instance;
- **Uso:** Acesso global único
- **Projeto:** GameController.instance

### Sprite
Imagem 2D para renderizar
- **Projeto:** Blocos, pinguim, bordas (sprites diferentes)

### SpriteRenderer
Componente que renderiza Sprite
- **Propriedade:** sprite (qual imagem), Color (tint)
- **Projeto:** IceBlock muda sprite com progresso dano

---

## T

### Transform
Componente de posição, rotação, escala
- **Propriedades:** position, rotation, scale
- **Todas:** Todos GameObjects têm Transform

### TriggerGameOver()
Método que finaliza jogo
- **Acionada:** Penguin.Update() quando Y < -5
- **Efeito:** Painel aparece, turnos param

### Turn (Turno)
Rodada de um jogador
- **Alternância:** currentPlayer = (currentPlayer == 1) ? 2 : 1
- **Controle:** GameController

---

## U

### UI (User Interface)
Interface do usuário
- **Elementos:** Canvas, Panel, Button, Text
- **Projeto:** GameOverPanel com WinnerText e RestartButton

### Update()
Função chamada todo frame (60x por segundo)
- **Uso:** Lógica de game loop
- **Projeto:** Penguin.Update() detecta queda

---

## V

### Vector2
Coordenada 2D (X, Y)
- **Uso:** Posições, direções
- **Projeto:** Vector2.up, down, left, right para BFS

---

## W

### WinCondition
Condição de vitória do jogo
- **Projeto:** "Quem não derrubar o pinguim vence"
- **Detecta:** Quando pinguim cai (outro jogador perde)

---

## Z

### Z-Position
Profundidade na tela (ordem de renderização)
- **Negativo:** Atrás (câmera em -10)
- **0+:** Na frente
- **Projeto:** Câmera em (0, 0, -10) para ver tudo

---

## 🎓 ACRÔNIMOS

| Sigla | Significado | Uso |
|-------|-----------|-----|
| **BFS** | Breadth-First Search | Busca conectividade |
| **UI** | User Interface | Painel, botões |
| **OOP** | Object-Oriented Programming | Estrutura código |
| **FPS** | Frames Per Second | Performance |
| **Hz** | Hertz | Frequência (60 Hz = 60 FPS) |
| **O(1)** | Operação Constante | HashSet é rápido |
| **FIFO** | First In First Out | Queue |
| **ID** | Identification | Identificador único |
| **API** | Application Programming Interface | Métodos públicos |

---

## 🔗 RELACIONAMENTOS

```
GameController (Singleton)
    ├─ HandleBlockClick(block)
    │  └─ block.Hit()
    │  └─ BoardManager.CheckStability()
    │     └─ IsConnectedToBorder() [BFS]
    │
    └─ TriggerGameOver(loserPlayer)
       └─ Mostrar painel GameOverPanel

Penguin.Update()
    ├─ Detecta Y < -5
    └─ GameController.TriggerGameOver()

IceBlock.OnMouseDown()
    └─ GameController.HandleBlockClick(this)

Physics2D
    ├─ OverlapPoint() [detecção de colisão]
    └─ Gravity (pinguim e blocos caem)
```

---

## 📖 TERMOS IMPORTANTES POR TÓPICO

### Física
- **Rigidbody2D** - Corpo físico
- **Collider2D** - Forma de colisão
- **Gravity** - Força para baixo
- **Drag** - Fricção
- **Body Type** - Tipo de movimento

### Detecção
- **OnMouseDown()** - Clique do mouse
- **Raycast** - Linha de detecção
- **OverlapPoint()** - Ponto de colisão
- **Collider** - Forma para detecção

### Estrutura
- **GameObject** - Entidade do jogo
- **Component** - Parte funcional
- **Script** - Lógica em código
- **Prefab** - Template reutilizável

### Algoritmos
- **BFS** - Busca em largura
- **Queue** - Fila FIFO
- **HashSet** - Conjunto único

### Padrões
- **Singleton** - Uma única instância
- **Observer** - Reação a eventos
- **State Machine** - Estados e transições

---

## 💡 DICAS DE MEMORIZAÇÃO

```
BFS = "Burst From Source"
  → Começa em um ponto, expande em círculos

FIFO = "First In, First Out"
  → Primeira coisa que entra é a primeira a sair (fila)

Raycast = "Ray blast"
  → Dispara raio invisível para detectar

Singleton = "Single ton"
  → Apenas UM getInstance() possível

Prefab = "Pre-fabricated"
  → Pré-construído, pronto para copiar
```

---

## 🎯 ORDEM TÍPICA DE EXECUÇÃO

```
1. Awake()         ← Inicializa componentes
2. Start()         ← Setup inicial
3. Update()        ← Game loop (60x/seg)
4. OnMouseDown()   ← Evento de clique
5. HandleClick()   ← Lógica de jogo
6. CheckStability()← Verifica física
7. Physics2D       ← Aplica gravidade
8. OnDestroy()     ← Limpeza
```

---

**Última atualização:** 2026-06-08

Use este glossário como referência enquanto implementa o jogo!

