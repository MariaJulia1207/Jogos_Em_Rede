# ✅ CHECKLIST INTERATIVO - Implementação Completa

## 📋 PARTE 1: PREPARAÇÃO DO PROJETO (15 minutos)

### ✓ Passo 1.1: Configurar Layers
```
[ ] Abrir Project Settings → Tags and Layers
[ ] Adicionar "IceLayer" (Layer 8)
[ ] Adicionar "BorderLayer" (Layer 9)
[ ] Adicionar "PenguinLayer" (Layer 10)
[ ] Verificar que aparecem em todos os GameObjects
```

### ✓ Passo 1.2: Configurar Câmera
```
[ ] Selecionar Main Camera
[ ] Transform Position: (0, 0, -10)
[ ] Camera → Orthographic: ✓
[ ] Camera → Size: 5
[ ] Camera → Background Color: Azul escuro (ou preto)
[ ] Testar que vê todo o tabuleiro no Scene view
```

### ✓ Passo 1.3: Preparar Canvas de UI
```
[ ] Criar Canvas (GameObject → 2D Object → UI → Canvas)
[ ] Canvas → Render Mode: Screen Space - Overlay
[ ] Dentro do Canvas, criar Panel
[ ] Renomear para "GameOverPanel"
[ ] Position: Centro (0, 0)
[ ] Size: (600, 400)
[ ] Image Color: Preto (alpha 0.8)
[ ] Image → SetActive(false) - IMPORTANTE!
```

### ✓ Passo 1.4: Adicionar Textos na UI
```
[ ] Dentro de GameOverPanel → 2D Object → Text
[ ] Renomear para "WinnerText"
[ ] Text: "Jogador X venceu! O pinguim caiu."
[ ] Font Size: 40
[ ] Color: Branco
[ ] Position: Topo do painel (y ≈ 150)
[ ] Alignment: Center

[ ] Dentro de GameOverPanel → 2D Object → Button
[ ] Renomear para "RestartButton"
[ ] Text: "REINICIAR"
[ ] Font Size: 30
[ ] Position: Embaixo (y ≈ -100)
```

---

## 📦 PARTE 2: CRIAR PINGUIM (10 minutos)

### ✓ Passo 2.1: GameObject do Pinguim
```
[ ] Right-click na Hierarquia → 2D Object → Sprites → Circle
[ ] Renomear para "Penguin"
[ ] Position: (0, 0.5, 0)
[ ] Scale: (1, 1, 1)
```

### ✓ Passo 2.2: Sprite Renderer
```
[ ] Componente Sprite Renderer já adicionado
[ ] Sprite: (escolha um sprite ou deixe Circle)
[ ] Color: Branco
[ ] Sorting Order: 1
```

### ✓ Passo 2.3: Collider
```
[ ] Adicionar componente "Circle Collider 2D"
[ ] Radius: 0.5
[ ] Is Trigger: false
```

### ✓ Passo 2.4: Rigidbody (IMPORTANTE!)
```
[ ] Adicionar componente "Rigidbody 2D"
[ ] Body Type: Dynamic
[ ] Gravity Scale: 1
[ ] Drag: 0
[ ] Angular Drag: 0
[ ] Constraints → Freeze Rotation Z: ✓ (para não girar)
[ ] Collision Detection: Continuous
```

### ✓ Passo 2.5: Script e Layer
```
[ ] Adicionar componente "Penguin.cs"
[ ] Layer: PenguinLayer
[ ] Teste: Ao pressionar Play, pinguim deve cair se não tiver suporte
```

---

## ❄️ PARTE 3: CRIAR PREFAB DE BLOCO (15 minutos)

### ✓ Passo 3.1: Criar Bloco Individual
```
[ ] Right-click na Hierarquia → 2D Object → Sprites → Square
[ ] Renomear para "IceBlock"
[ ] Position: (0, 0, 0) (temporário)
[ ] Scale: (1, 1, 1)
```

### ✓ Passo 3.2: Sprite Renderer
```
[ ] Sprite: (sprite de gelo ou quadrado branco)
[ ] Color: Azul claro (#87CEEB) ou branco
[ ] Sorting Order: 0
```

### ✓ Passo 3.3: Collider
```
[ ] Adicionar componente "Box Collider 2D"
[ ] Size: (1, 1)
[ ] Is Trigger: false
```

### ✓ Passo 3.4: Script e Sprites
```
[ ] Adicionar componente "IceBlock.cs"
[ ] Damage Sprites: 
  [ ] Element 0: [Sprite Intacto]
  [ ] Element 1: [Sprite Dano Leve]
  [ ] Element 2: [Sprite Dano Médio]
  [ ] Element 3: [Sprite Dano Pesado] (opcional)
[ ] Layer: IceLayer
```

### ✓ Passo 3.5: Transformar em Prefab
```
[ ] Selecionar "IceBlock" na Hierarquia
[ ] Drag & Drop para Assets/Prefabs/
[ ] Confirmar criar Prefab
[ ] Delete de IceBlock da Hierarquia (agora é prefab)
[ ] Verificar que Assets/Prefabs/IceBlock.prefab existe
```

---

## 🏗️ PARTE 4: CRIAR TABULEIRO (10 minutos)

### ✓ Passo 4.1: Container do Tabuleiro
```
[ ] Right-click na Hierarquia → Create Empty
[ ] Renomear para "Board"
[ ] Position: (0, 0, 0)
[ ] Reset Transform
```

### ✓ Passo 4.2: Instanciar Blocos (2x5 grid)
```
Usar Prefab em Assets/Prefabs/IceBlock.prefab

LINHA 1 (Y = 1.0):
[ ] Block_1: Position (-2, 1, 0) → Drag IceBlock prefab
[ ] Block_2: Position (-1, 1, 0)
[ ] Block_3: Position (0, 1, 0)
[ ] Block_4: Position (1, 1, 0)
[ ] Block_5: Position (2, 1, 0)

LINHA 2 (Y = -0.1):
[ ] Block_6: Position (-2, -0.1, 0)
[ ] Block_7: Position (-1, -0.1, 0)
[ ] Block_8: Position (0, -0.1, 0)
[ ] Block_9: Position (1, -0.1, 0)
[ ] Block_10: Position (2, -0.1, 0)

VERIFICAÇÃO:
[ ] Contar total: 10 blocos
[ ] Todos dentro de Board
[ ] Pinguim visível no centro (0, 0.5)
[ ] Espaço bem distribuído
```

---

## 🚧 PARTE 5: CRIAR BORDAS (10 minutos)

### ✓ Passo 5.1: Borda Superior
```
[ ] Right-click na Hierarquia → Create Empty
[ ] Renomear para "Border_Top"
[ ] Position: (0, 2, 0)
[ ] Adicionar "Box Collider 2D"
  [ ] Size: (6, 0.5)
  [ ] Is Trigger: false
[ ] Layer: BorderLayer
```

### ✓ Passo 5.2: Borda Inferior
```
[ ] Duplicar Border_Top (Ctrl+D)
[ ] Renomear para "Border_Bottom"
[ ] Position: (0, -1.5, 0)
[ ] Layer: BorderLayer
```

### ✓ Passo 5.3: Borda Esquerda
```
[ ] Duplicar Border_Top
[ ] Renomear para "Border_Left"
[ ] Position: (-3, 0.5, 0)
[ ] Box Collider 2D → Size: (0.5, 3)
[ ] Layer: BorderLayer
```

### ✓ Passo 5.4: Borda Direita
```
[ ] Duplicar Border_Top
[ ] Renomear para "Border_Right"
[ ] Position: (3, 0.5, 0)
[ ] Box Collider 2D → Size: (0.5, 3)
[ ] Layer: BorderLayer
```

### ✓ Verificação de Bordas
```
[ ] Scene view: 4 bordas visíveis
[ ] Cercam completamente os blocos
[ ] Pinguim visível no centro
```

---

## 🎮 PARTE 6: CRIAR GAME MANAGER (10 minutos)

### ✓ Passo 6.1: Criar GameObject Manager
```
[ ] Right-click na Hierarquia → Create Empty
[ ] Renomear para "GameManager"
[ ] Position: (0, 0, 0)
```

### ✓ Passo 6.2: Adicionar GameController.cs
```
[ ] Adicionar componente "GameController.cs"
[ ] Public Variables visivelmente vazias na Inspector
```

### ✓ Passo 6.3: Adicionar BoardManager.cs
```
[ ] Adicionar componente "BoardManager.cs"
[ ] Ice Layer: IceLayer (Layer 8)
[ ] Border Layer: BorderLayer (Layer 9)
[ ] Grid Size: 1.1
```

### ✓ Passo 6.4: Conectar Referências (IMPORTANTE!)
```
No GameController:
[ ] Game Over Panel: Arraste "GameOverPanel" da UI para este field
[ ] Winner Text: Arraste "WinnerText" (texto dentro de GameOverPanel)

Resultado esperado:
[ ] Ambos os fields mostram o objeto correto
[ ] Sem erros na Console
```

---

## 🎯 PARTE 7: CONFIGURAR UI - BOTÃO (5 minutos)

### ✓ Passo 7.1: Conectar Botão RESTART
```
[ ] Selecionar "RestartButton" na Canvas
[ ] Componente "Button" (já existe)
[ ] Rolar para "On Click ()" event
[ ] Clicar em "+" para adicionar listener
[ ] Drag "GameManager" para o campo Object
[ ] Dropdown: GameController → RestartGame()
[ ] Verificar que aparece "GameController.RestartGame()"
```

### ✓ Passo 7.2: Testar Botão
```
[ ] Pressionar Play
[ ] Abrir Console
[ ] Não deve haver erros ainda
```

---

## 🧪 PARTE 8: TESTAR JOGO (15 minutos)

### ✓ Fase 1: Teste Básico
```
[ ] Pressionar Play
[ ] Cena carrega sem erros
[ ] Pinguim visível no centro
[ ] 10 blocos visíveis
[ ] Bordas ao redor
[ ] Console: Nenhum erro (vermelho)
```

### ✓ Fase 2: Teste de Cliques
```
[ ] Clicar em bloco qualquer
[ ] Console mostra: "Turno do jogador: 2"
[ ] Bloco muda sprite (deve parecer danificado)
[ ] Clicar novamente no mesmo bloco
[ ] Sprite muda novamente
[ ] Clicar terceira vez
[ ] Bloco desaparece completamente
```

### ✓ Fase 3: Teste de Turnos
```
[ ] Clicar em 2-3 blocos diferentes
[ ] Console alternando entre "Turno: 1" e "Turno: 2"
[ ] Se não alterna: verifique GameController singleton
```

### ✓ Fase 4: Teste de Física - Blocos Caem
```
[ ] Clicar em blocos estrategicamente
[ ] Clique em bloco que faz outro ficar isolado
[ ] Console deve mostrar: "Bloco ... está caindo!"
[ ] Bloco isolado deve se mover para baixo (física)
[ ] Se não cai: verifique Rigidbody2D foi adicionado
```

### ✓ Fase 5: Teste de Queda do Pinguim
```
[ ] Continue o jogo até derrubar pinguim
[ ] OU: Use ferramenta de movimento para abaixar pinguim (debug)
[ ] Quando Y < -5:
  [ ] Console mostra: "Pinguim caiu! Jogador X perdeu!"
  [ ] GameOverPanel aparece
  [ ] WinnerText mostra mensagem de vitória
```

### ✓ Fase 6: Teste de Reinício
```
[ ] Clicar em botão RESTART
[ ] Cena recarrega
[ ] Tudo volta ao normal
[ ] Pode jogar novamente
```

---

## 🐛 PARTE 9: DEBUG - Se Algo Não Funcionar

### ❌ "Clique em bloco não faz nada"
```
☐ Verificar se IceBlock tem "Box Collider 2D"
☐ Verificar se OnMouseDown() existe em IceBlock.cs
☐ Verificar se GameController.instance não é null
  └─ Adicione no GameController.Awake(): if (instance != null) return;
☐ Console → Clicar em bloco deve mostrar erro se houver
☐ Testar com Debug.Log("Clique em " + name);
```

### ❌ "Bloco não muda sprite"
```
☐ Verificar se damageSprites está preenchido (4 sprites)
☐ Verificar se Sprite Renderer tem sprite inicial
☐ Adicionar Debug.Log na função Hit()
☐ Verificar que sr = GetComponent<SpriteRenderer>();
```

### ❌ "Blocos não caem"
```
☐ Verificar se bloco isolado tem Rigidbody2D APÓS queda
  └─ Verificar console: "Bloco ... está caindo!"
☐ Se não mostra mensagem: CheckStability() não rodando
☐ Verificar se BoardManager existe na cena
☐ Verificar se Physics2D settings corretos:
  └─ Edit → Project Settings → Physics2D
  └─ Gravity: (0, -9.81)
☐ Se bloco cai ANTES de ser isolado:
  └─ Verificar IsConnectedToBorder() logic
  └─ Adicionar Debug logs em IsConnectedToBorder
```

### ❌ "Pinguim não cai"
```
☐ Verificar se Pinguim tem Rigidbody2D
☐ Verificar se Body Type = Dynamic
☐ Verificar se Gravity Scale > 0
☐ Se tudo está OK:
  └─ Tirar blocos de baixo do pinguim manualmente (no Scene)
  └─ Pressionar Play
  └─ Pinguim deve cair
```

### ❌ "Game Over não aparece"
```
☐ Verificar GameOverPanel está desativado inicialmente (unchecked)
☐ Verificar se GameController tem referência correta do painel
☐ Verificar se WinnerText está dentro do GameOverPanel
☐ Adicionar Debug.Log em TriggerGameOver():
  Debug.Log("Game Over! Vencedor: " + winner);
☐ Se pinguim cai mas painel não abre:
  └─ gameOverPanel.SetActive(true); pode estar com erro
  └─ Verifique null reference
```

### ❌ "Botão RESTART não funciona"
```
☐ Verificar se Button tem "On Click()" configurado
☐ Verificar se apontando para GameController.RestartGame()
☐ Testar clicando no botão (Scene view)
☐ Se não funciona:
  └─ Recrear On Click() listener
  └─ Reatribuir referências manualmente
```

---

## ✨ PARTE 10: MELHORIAS OPCIONAIS

### 🎨 Visuais
```
[ ] Adicionar cores aos blocos (degradê)
[ ] Adicionar sombra do pinguim
[ ] Adicionar efeito visual quando bloco é clicado
[ ] Adicionar partículas quando bloco é destruído
```

### 🔊 Som
```
[ ] Adicionar som ao clicar em bloco
[ ] Adicionar som quando bloco é destruído
[ ] Adicionar música de fundo
[ ] Adicionar som de game over
```

### 📊 UI
```
[ ] Adicionar indicador de turno (texto: "Turno: Jogador 1")
[ ] Adicionar timer se quiser (tempo máximo por turno)
[ ] Adicionar contador de blocos restantes
[ ] Adicionar mini-mapa
```

### 🎮 Gameplay
```
[ ] Adicionar niveis de dificuldade (mais/menos blocos)
[ ] Adicionar power-ups (recuperar bloco, escudo, etc)
[ ] Adicionar limites de movimentos por turno
[ ] Adicionar tabuleiros diferentes (formações diferentes)
```

### 🌐 Rede
```
[ ] Integrar Photon para multiplayer online
[ ] Sincronizar cliques entre jogadores
[ ] Sincronizar posição do pinguim
[ ] Implementar chat entre jogadores
```

---

## 📋 CHECKLIST FINAL ANTES DO DEPLOY

```
CENA:
[ ] Sem erros vermelhos na Console
[ ] Sem warnings (amarelos) importantes
[ ] Todos os GameObjects nomeados corretamente
[ ] Hierarquia bem organizada
[ ] Sem GameObjects vazios desnecessários

SCRIPTS:
[ ] Todos os scripts compilam sem erro
[ ] Todos os campos públicos preenchidos
[ ] Nenhum null reference warning
[ ] Console limpo (só warnings aceitáveis)

GAMEPLAY:
[ ] Cliques funcionam em blocos
[ ] Turnos alternando corretamente
[ ] Blocos caem quando isolados
[ ] Pinguim cai quando sem suporte
[ ] Game Over dispara ao pinguim cair
[ ] Botão RESTART funciona

PERFORMANCE:
[ ] FPS estável (60+)
[ ] Sem lag ao clicar
[ ] Sem lag ao blocos caírem
[ ] Sem lag ao destruir blocos

TESTES:
[ ] Testou 3+ partidas completas
[ ] Testou casos extremos (todos blocos destruídos, etc)
[ ] Testou múltiplos restart
[ ] Sem crashes ou exceções
```

---

## 🎉 PRONTO PARA JOGAR!

Se completou todos estes passos:

✅ **PARABÉNS!** Seu jogo "Quebra Gelo" está funcional!

**Próximos passos sugeridos:**
1. Testar com amigos
2. Coletar feedback
3. Implementar melhorias da seção 10
4. Publicar no itch.io ou Unity Play
5. Integrar multiplayer online (Photon/Mirror)

**Links úteis:**
- Documentação Unity: docs.unity3d.com
- Photon (Multiplayer): photonengine.com
- Itch.io (Publication): itch.io

