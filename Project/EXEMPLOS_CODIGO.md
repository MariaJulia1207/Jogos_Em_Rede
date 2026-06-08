# 💻 Exemplos de Código Detalhados - Quebra Gelo

## 1. GameController.cs - GAME MANAGER

```csharp
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Gerencia o estado geral do jogo, turnos e condições de vitória.
/// Implementa padrão Singleton para acesso global.
/// </summary>
public class GameController : MonoBehaviour
{
    // ========== SINGLETON ==========
    // Garante que existe apenas uma instância
    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    // ========== ESTADO DO JOGO ==========
    /// <summary>
    /// Jogador atual: 1 ou 2
    /// </summary>
    public int currentPlayer = 1;

    /// <summary>
    /// Flag para evitar ações após game over
    /// </summary>
    public bool isGameOver = false;

    // ========== REFERÊNCIAS DE UI ==========
    /// <summary>
    /// Painel exibido quando o jogo termina
    /// Atribua na Unity Inspector
    /// </summary>
    public GameObject gameOverPanel;

    /// <summary>
    /// Texto que mostra qual jogador venceu
    /// Atribua na Unity Inspector (Text dentro do gameOverPanel)
    /// </summary>
    public Text winnerText;

    // ========== MÉTODOS ==========

    /// <summary>
    /// Chamado quando um bloco é clicado.
    /// Executa lógica do bloco, verifica estabilidade e troca turno.
    /// </summary>
    /// <param name="block">Bloco que foi clicado</param>
    public void HandleBlockClick(IceBlock block)
    {
        // Se o jogo já terminou, ignora cliques
        if (isGameOver) return;

        // 1. EXECUTA LÓGICA DO BLOCO
        // Incrementa dano, muda sprite ou destrói
        block.Hit();

        // 2. VERIFICA ESTABILIDADE
        // Obtém o BoardManager e checa quais blocos caem
        FindObjectOfType<BoardManager>().CheckStability();

        // 3. TROCA O TURNO
        // Se era Player 1, muda para Player 2 (e vice-versa)
        currentPlayer = (currentPlayer == 1) ? 2 : 1;

        // Debug: Mostra no console qual é o próximo jogador
        Debug.Log("Turno do jogador: " + currentPlayer);
    }

    /// <summary>
    /// Chamado quando o pinguim cai.
    /// Exibe painel de vitória com nome do vencedor.
    /// </summary>
    /// <param name="loserPlayer">Qual jogador causou a queda (perdeu)</param>
    public void TriggerGameOver(int loserPlayer)
    {
        // Define que o jogo terminou
        isGameOver = true;

        // Calcula o vencedor (o outro jogador)
        int winner = (loserPlayer == 1) ? 2 : 1;

        // Exibe painel de game over
        gameOverPanel.SetActive(true);

        // Escreve mensagem de vitória
        // Se Jogador 1 perdeu, Jogador 2 venceu
        winnerText.text = "Jogador " + winner + " venceu! O pinguim caiu.";
    }

    /// <summary>
    /// Reinicia a cena (chamado pelo botão REINICIAR)
    /// </summary>
    public void RestartGame()
    {
        // Carrega a mesma cena do zero
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
```

### Como usar no Inspector:
```
GameController (Script Component):
├─ Current Player: 1
├─ Is Game Over: false
├─ Game Over Panel: [Arraste GameOverPanel da UI]
└─ Winner Text: [Arraste Text dentro de GameOverPanel]
```

---

## 2. BoardManager.cs - GERENCIADOR DE TABULEIRO

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Gerencia a física dos blocos no tabuleiro.
/// Verifica quais blocos estão conectados à borda (seguros)
/// e quais devem cair (isolados).
/// </summary>
public class BoardManager : MonoBehaviour
{
    // ========== CONFIGURAÇÕES ==========
    /// <summary>
    /// Layer que contém os blocos de gelo
    /// </summary>
    public LayerMask iceLayer;

    /// <summary>
    /// Layer que contém as bordas do tabuleiro
    /// </summary>
    public LayerMask borderLayer;

    /// <summary>
    /// Espaço entre blocos (debe ser maior que tamanho do sprite)
    /// Exemplo: sprite 1.0 → gridSize 1.1 (pequeno espaço)
    /// </summary>
    public float gridSize = 1.1f;

    // ========== MÉTODOS ==========

    /// <summary>
    /// Verifica estabilidade de TODOS os blocos após uma ação.
    /// Blocos isolados (não conectados à borda) ganham gravidade.
    /// </summary>
    public void CheckStability()
    {
        // Obtém TODOS os blocos ativos na cena
        IceBlock[] allBlocks = FindObjectsOfType<IceBlock>();

        // Verifica cada bloco individualmente
        foreach (var block in allBlocks)
        {
            // Se o bloco NÃO está conectado à borda...
            if (!IsConnectedToBorder(block.transform.position))
            {
                // ...Adiciona física (Rigidbody2D) para ele cair
                block.gameObject.AddComponent<Rigidbody2D>();
                
                Debug.Log("Bloco em " + block.transform.position + " está caindo!");
            }
        }
    }

    /// <summary>
    /// Verifica se um bloco está conectado a uma borda usando BFS.
    /// BFS = Busca em Largura (exploração em camadas)
    /// </summary>
    /// <param name="position">Posição do bloco a verificar</param>
    /// <returns>true se conectado à borda, false se isolado</returns>
    bool IsConnectedToBorder(Vector2 position)
    {
        // ========== INICIALIZAÇÃO DA BUSCA ==========
        // Queue = Fila (FIFO: First In, First Out)
        Queue<Vector2> queue = new Queue<Vector2>();
        // Set = Conjunto para evitar revisitar mesma posição
        HashSet<Vector2> visited = new HashSet<Vector2>();

        // Começa a busca a partir do bloco
        queue.Enqueue(position);

        // ========== DIREÇÕES DE MOVIMENTO ==========
        // Checamos 4 direções: cima, baixo, esquerda, direita
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        // ========== BUSCA EM LARGURA ==========
        while (queue.Count > 0)
        {
            // Remove o primeiro elemento da fila
            Vector2 current = queue.Dequeue();

            // Verifica cada uma das 4 direções
            foreach (var dir in directions)
            {
                // Calcula posição do vizinho
                // Usa gridSize para pular até o próximo bloco
                Vector2 neighbor = current + (dir * gridSize);

                // ========== CHECAGEM 1: É BORDA? ==========
                // Se encontrar uma borda, o bloco está SEGURO!
                if (Physics2D.OverlapPoint(neighbor, borderLayer))
                {
                    return true; // ✓ Conectado à borda
                }

                // ========== CHECAGEM 2: É OUTRO BLOCO? ==========
                // Se encontrar outro bloco não visitado, continua a busca
                if (Physics2D.OverlapPoint(neighbor, iceLayer) && !visited.Contains(neighbor))
                {
                    // Marca como visitado (evita loop infinito)
                    visited.Add(neighbor);
                    // Adiciona à fila para explorar seus vizinhos
                    queue.Enqueue(neighbor);
                }
            }
        }

        // Se a busca terminou sem encontrar borda → ISOLADO
        return false; // ✗ Não conectado à borda (vai cair)
    }

    // ========== EXEMPLO DE BFS ==========
    /*
    Tabuleiro:
    B1 - B2 - B3
    B4 - X  - B5
    B6 - B7 - B8
    
    Verificando X (posição 0,0):
    
    Iteração 1: current = (0, 0) [X]
    - Vizinhos: (0,1)=B2, (0,-1)=B7, (-1,0)=B4, (1,0)=B5
    - Encontra blocos, adiciona à fila
    - Nenhum é borda
    
    Iteração 2: current = (0, 1) [B2]
    - Vizinhos: (0,2)=B3, (0,0)=X (já visitado), (-1,1)=B1, (1,1)=B5 (já visitado)
    - Encontra B3, adiciona à fila
    
    Iteração 3: current = (0, -1) [B7]
    - Vizinhos: (0,-2)=?, ...
    - Continua procurando borda
    
    ... continua até explorar tudo ...
    
    Resultado: Se nenhuma borda encontrada → return false → bloco cai
    */
}
```

### Como usar no Inspector:
```
Board (GameObject):
└── BoardManager (Script Component):
    ├─ Ice Layer: IceLayer (ou Layer 8)
    ├─ Border Layer: BorderLayer (ou Layer 9)
    └─ Grid Size: 1.1
```

---

## 3. IceBlock.cs - BLOCO DE GELO

```csharp
using UnityEngine;

/// <summary>
/// Representa um bloco de gelo individual.
/// Gerencia progresso de dano (0 → 1 → 2 → destruição).
/// Detecta cliques e avisa o GameController.
/// </summary>
public class IceBlock : MonoBehaviour
{
    // ========== SPRITES DE DANO ==========
    /// <summary>
    /// Array com 4 sprites representando estados de dano:
    /// [0] = Intacto (branco)
    /// [1] = Dano Leve (poucas rachaduras)
    /// [2] = Dano Médio (mais rachaduras)
    /// [3] = Dano Pesado (muito danificado)
    /// 
    /// Atribua na Unity Inspector arrastando sprites
    /// </summary>
    public Sprite[] damageSprites;

    // ========== ESTADO INTERNO ==========
    /// <summary>
    /// Contador de cliques/danos
    /// 0 = Intacto
    /// 1 = Dano leve
    /// 2 = Dano médio
    /// 3+ = Destruído
    /// </summary>
    private int hitCount = 0;

    /// <summary>
    /// Referência ao componente SpriteRenderer
    /// Usado para mudar a imagem visual do bloco
    /// </summary>
    private SpriteRenderer sr;

    // ========== INICIALIZAÇÃO ==========
    /// <summary>
    /// Executado quando o GameObject é criado/instanciado
    /// Obtém referência ao SpriteRenderer
    /// </summary>
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // ========== MÉTODOS PÚBLICOS ==========

    /// <summary>
    /// Aplica um dano ao bloco.
    /// Incrementa hitCount e muda sprite ou destrói.
    /// </summary>
    public void Hit()
    {
        // Incrementa contador de danos
        hitCount++;

        Debug.Log("Bloco " + name + " sofreu dano. hitCount = " + hitCount);

        // ========== LÓGICA DE DESTRUIÇÃO ==========
        if (hitCount >= 3)
        {
            // Se 3 ou mais cliques → DESTRUIR
            // O bloco desaparece completamente da cena
            Destroy(gameObject);

            Debug.Log("Bloco " + name + " foi destruído!");
        }
        else
        {
            // Se menos de 3 cliques → MUDAR SPRITE
            // Mostra sprite correspondente ao nível de dano
            sr.sprite = damageSprites[hitCount];

            Debug.Log("Bloco " + name + " mudou para sprite " + hitCount);
        }
    }

    // ========== DETECÇÃO DE CLIQUE ==========

    /// <summary>
    /// Executado automaticamente pela Unity quando o mouse clica neste GameObject.
    /// Requer: BoxCollider2D ou CircleCollider2D no GameObject.
    /// 
    /// Como funciona:
    /// 1. Unity faz raycast da câmera até o ponto clicado
    /// 2. Se colidir com este collider, OnMouseDown() é chamado
    /// 3. Avisamos o GameController que esse bloco foi clicado
    /// </summary>
    private void OnMouseDown()
    {
        // Avisa o Game Manager que este bloco foi clicado
        // GameController processa o click e troca turno
        GameController.instance.HandleBlockClick(this);
    }

    // ========== EXEMPLO DE CICLO DE VIDA ==========
    /*
    BLOCO CRIADO:
    ├─ hitCount = 0
    ├─ sprite = damageSprites[0] (Intacto)
    └─ pronto para cliques

    1º CLIQUE:
    ├─ Hit() chamado
    ├─ hitCount = 1
    ├─ sprite = damageSprites[1] (Dano Leve)
    └─ bloco visualmente danificado

    2º CLIQUE:
    ├─ Hit() chamado
    ├─ hitCount = 2
    ├─ sprite = damageSprites[2] (Dano Médio)
    └─ mais danificado ainda

    3º CLIQUE:
    ├─ Hit() chamado
    ├─ hitCount = 3
    ├─ hitCount >= 3? SIM
    ├─ Destroy(gameObject)
    └─ Bloco desaparece da cena
    */
}
```

### Como usar no Inspector:
```
IceBlock (Prefab):
├─ Transform
│  └─ Position: (seu grid)
│
├─ Sprite Renderer
│  ├─ Sprite: (sua imagem de bloco)
│  └─ Color: Branco (ou azul claro)
│
├─ Box Collider 2D
│  ├─ Size: (1, 1)
│  └─ Is Trigger: false
│
└─ IceBlock (Script)
   └─ Damage Sprites:
      ├─ Element 0: [Sprite Intacto]
      ├─ Element 1: [Sprite Leve]
      ├─ Element 2: [Sprite Médio]
      └─ Element 3: [Sprite Pesado (opcional)]
```

---

## 4. Penguin.cs - PINGUIM

```csharp
using UnityEngine;

/// <summary>
/// Representa o pinguim que fica sobre os blocos.
/// Detecta quando cai e aciona Game Over.
/// </summary>
public class Penguin : MonoBehaviour
{
    // ========== CONSTANTES ==========
    /// <summary>
    /// Y position limite para considerar que caiu
    /// Abaixo deste valor, o pinguim está "fora do mapa"
    /// </summary>
    private const float FALL_THRESHOLD = -5f;

    // ========== UPDATE ==========

    /// <summary>
    /// Executado a cada frame
    /// Verifica se o pinguim caiu do mapa
    /// </summary>
    void Update()
    {
        // ========== DETECÇÃO DE QUEDA ==========
        // Se a posição Y do pinguim é menor que -5...
        if (transform.position.y < FALL_THRESHOLD && !GameController.instance.isGameOver)
        {
            // O pinguim caiu! Jogo termina
            // Passa qual jogador era a vez (ele perdeu)
            GameController.instance.TriggerGameOver(GameController.instance.currentPlayer);

            Debug.Log("Pinguim caiu! Jogador " + GameController.instance.currentPlayer + " perdeu!");
        }
    }

    // ========== LÓGICA DE QUEDA ==========
    /*
    Quando um bloco é destruído:
    1. Bloco é removido (Destroy)
    2. Pinguim perde apoio
    3. Rigidbody2D faz pinguim cair naturalmente (gravidade)
    4. Y position diminui continuamente
    5. Quando Y < -5: Update() detecta
    6. TriggerGameOver() é chamado
    
    Visualização:
    
    ANTES (Blocos intactos):
         P
       [B][B]
       [B][B]
       ─────  Bordas
    
    DURANTE (Blocos caindo):
         P
        / \
       [B][B] ← alguns caem
           [B]
    
    DEPOIS (Queda final):
         P (caindo)
         |
         ▼ y = -2
         |
         ▼ y = -5 (LIMITE!)
         |
         ▼ y = -10 (GAME OVER!)
    */
}
```

### Como usar no Inspector:
```
Penguin (GameObject):
├─ Transform
│  ├─ Position: (0, 0.5, 0)
│  └─ Scale: (1, 1, 1)
│
├─ Sprite Renderer
│  ├─ Sprite: [Seu sprite do pinguim]
│  └─ Color: Branco
│
├─ Circle Collider 2D
│  ├─ Radius: 0.5
│  └─ Is Trigger: false
│
├─ Rigidbody 2D
│  ├─ Body Type: Dynamic
│  ├─ Gravity Scale: 1
│  ├─ Constraints: Freeze Rotation Z
│  └─ Layer: PenguinLayer
│
└─ Penguin (Script)
   └─ (Nenhuma configuração necessária)
```

---

## 5. UI Setup - Canvas e Painel de Game Over

```csharp
// PSEUDO-CÓDIGO para referência (feito na UI, não em script)

Canvas:
└── GameOverPanel (Image)
    ├─ Position: Centro da tela
    ├─ Size: 600x400
    ├─ Color: Preto com Alpha 0.8
    ├─ Active: false (desativado inicialmente)
    │
    ├── WinnerText (Text)
    │  ├─ Position: Topo do painel
    │  ├─ Font Size: 40
    │  ├─ Text: "Jogador X venceu! O pinguim caiu."
    │  └─ Color: Branco
    │
    └── RestartButton (Button)
       ├─ Position: Embaixo do texto
       ├─ Text: "REINICIAR"
       ├─ Font Size: 30
       ├─ On Click():
       │  └─ GameManager → TriggerGameOver() → RestartGame()
       └─ Colors:
          ├─ Normal: Azul
          ├─ Pressed: Azul Escuro
          └─ Hovered: Azul Claro
```

---

## 6. Exemplo de Instanciação de Blocos (Script Helper)

```csharp
using UnityEngine;

/// <summary>
/// Script HELPER para criar o tabuleiro automaticamente.
/// Coloque este script no Board vazio e arraste o prefab.
/// Execute uma vez, depois delete este script.
/// </summary>
public class BoardGenerator : MonoBehaviour
{
    public GameObject iceBlockPrefab; // Arraste o prefab aqui
    public int columns = 5;           // 5 colunas
    public int rows = 2;              // 2 linhas
    public float spacing = 1.1f;      // Espaço entre blocos

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        float startX = -(columns / 2.0f) * spacing;
        float startY = (rows / 2.0f) * spacing;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calcula posição do bloco
                Vector3 position = new Vector3(
                    startX + (col * spacing),
                    startY - (row * spacing),
                    0
                );

                // Instancia bloco nessa posição
                Instantiate(iceBlockPrefab, position, Quaternion.identity, transform);
            }
        }

        Debug.Log("Tabuleiro gerado com " + (columns * rows) + " blocos!");
    }
}
```

---

## 7. Fluxo Completo - Checklist

```csharp
// ANTES DO JOGO COMEÇAR:
☐ Layers criadas (IceLayer, BorderLayer, PenguinLayer)
☐ Câmera em ortográfico, Size 5
☐ Pinguim criado com Rigidbody2D Dynamic
☐ Blocos com IceBlock.cs e sprites configurados
☐ Bordas criadas com BorderLayer
☐ GameController com referências corretas
☐ BoardManager com Layers e gridSize corretos
☐ Canvas com GameOverPanel (desativado)
☐ Botão RESTART conectado ao método RestartGame()

// DURANTE O JOGO:
☐ Clique em bloco → OnMouseDown() dispara
☐ GameController.HandleBlockClick() processa
☐ IceBlock.Hit() muda sprite ou destrói
☐ BoardManager.CheckStability() verifica queda
☐ Blocos isolados ganham Rigidbody2D e caem
☐ Console mostra "Turno do jogador X"
☐ Turnos alternando entre 1 e 2

// QUANDO PINGUIM CAI:
☐ Penguin.Update() detecta Y < -5
☐ GameController.TriggerGameOver() chamado
☐ Game Over Panel ativa
☐ WinnerText mostra vencedor
☐ Botão RESTART disponível

// AO CLICAR RESTART:
☐ RestartGame() carrega cena
☐ Tudo volta ao estado inicial
```


