# ⚡ Setup Prático Passo-a-Passo - Quebra Gelo

## Fase 1: Preparação da Cena (5 minutos)

### 1.1 Criar Layers
```
Edit → Project Settings → Tags and Layers

Adicione 3 layers:
- IceLayer (Layer 8)
- BorderLayer (Layer 9)  
- PenguinLayer (Layer 10)
```

### 1.2 Configurar Câmera
```
Main Camera:
- Orthographic: ✓
- Size: 5
- Position: (0, 0, -10)
```

### 1.3 Criar Canvas para UI
```
Right-click em Canvas → 2D Object → Panel (espaço vazio)
Renomeie para "GameOverPanel"
- Desative inicialmente (unchecked)
- Cor de fundo: preto com alpha 0.8

Dentro de GameOverPanel:
- Adicione Text (UI) → Renomeie "WinnerText"
- Texto: "Jogador 1 venceu!"
- Font Size: 40
```

---

## Fase 2: Criar o Pinguim (3 minutos)

### 2.1 Criar GameObject
```
Right-click na hierarquia → 2D Object → Sprites → Circle
Renomeie: "Penguin"
```

### 2.2 Configurar Components
```
Transform:
- Position: (0, 0, 0)
- Scale: (1, 1, 1)

Sprite Renderer:
- Sprite: (seu sprite do pinguim ou círculo)
- Color: Branco

Circle Collider 2D:
- Radius: 0.5

Rigidbody 2D:
- Body Type: Dynamic
- Gravity Scale: 1
- Constraints: Freeze Rotation Z
- Layer: PenguinLayer

Script:
- Adicione componente Penguin.cs
```

---

## Fase 3: Criar Prefab do Bloco de Gelo (5 minutos)

### 3.1 Criar Bloco Individual
```
Right-click na hierarquia → 2D Object → Sprites → Square
Renomeie: "IceBlock"
```

### 3.2 Configurar Bloco
```
Transform:
- Scale: (1, 1, 1)

Sprite Renderer:
- Sprite: (quadrado branco ou seu sprite)
- Color: Azul claro (#87CEEB)

Box Collider 2D:
- Size: (1, 1)
- Is Trigger: false

Script:
- Adicione IceBlock.cs
- damageSprites[]: Arraste 4 sprites (intacto, leve, médio, quebrado)

Layer: IceLayer
```

### 3.3 Criar Prefab
```
Arraste "IceBlock" para Assets/Prefabs/
Delete do projeto (agora é prefab)
```

---

## Fase 4: Criar o Tabuleiro (5 minutos)

### 4.1 Criar Container
```
Right-click na hierarquia → Create Empty
Renomeie: "Board"
Position: (0, 0, 0)
```

### 4.2 Instanciar Blocos
```
Dentro de Board, crie grid de blocos:

Linha 1 (Y = 1):
Block_1: (-2, 1, 0)
Block_2: (-1, 1, 0)
Block_3: (0, 1, 0)    ← PINGUIM AQUI
Block_4: (1, 1, 0)
Block_5: (2, 1, 0)

Linha 2 (Y = -0.1):
Block_6: (-2, -0.1, 0)
Block_7: (-1, -0.1, 0)
Block_8: (0, -0.1, 0)
Block_9: (1, -0.1, 0)
Block_10: (2, -0.1, 0)

(gridSize = 1.1 → espaçamento automático)
```

---

## Fase 5: Criar Bordas (3 minutos)

### 5.1 Borda Superior
```
Right-click → Create Empty
Renomeie: "Border_Top"
Position: (0, 2, 0)

Box Collider 2D:
- Size: (6, 0.5)
- Is Trigger: false

Layer: BorderLayer
```

### 5.2 Borda Inferior
```
Duplica Border_Top → Border_Bottom
Position: (0, -1.5, 0)
```

### 5.3 Borda Esquerda
```
Duplica Border_Top → Border_Left
Position: (-3, 0.5, 0)
Box Collider 2D Size: (0.5, 3)
```

### 5.4 Borda Direita
```
Duplica Border_Top → Border_Right
Position: (3, 0.5, 0)
Box Collider 2D Size: (0.5, 3)
```

---

## Fase 6: Criar Game Manager (2 minutos)

### 6.1 Criar Manager
```
Right-click na hierarquia → Create Empty
Renomeie: "GameManager"
```

### 6.2 Adicionar Script
```
Adicione componente GameController.cs

Board Manager:
- Arraste Board para referência

Game Over Panel:
- Arraste GameOverPanel da UI

Winner Text:
- Arraste WinnerText da UI
```

### 6.3 Adicionar BoardManager
```
Board → Adicione componente BoardManager.cs

Ice Layer: IceLayer (Layer 8)
Border Layer: BorderLayer (Layer 9)
Grid Size: 1.1
```

---

## Fase 7: Testar o Jogo (2 minutos)

### 7.1 Pressionar Play
```
Clique em Play
Tente clicar em blocos
Observe:
- Bloco muda sprite ✓
- Após 3 cliques, desaparece ✓
- Turno alternando no console ✓
```

### 7.2 Testar Queda
```
Clique em blocos estrategicamente
Quando um bloco fica isolado, deve cair
Pinguim fica pendurado por blocos conectados
```

### 7.3 Testar Game Over
```
Cause a queda do pinguim
Painel de game over deve aparecer
Teste o botão de restart
```

---

## Fase 8: Adicionar Botões de UI (3 minutos)

### 8.1 Botão de Reiniciar
```
Inside GameOverPanel → 2D Object → Button
Renomeie: "RestartButton"

Text: "REINICIAR"
Position: Centro do painel

Script Button:
- On Click() → GameController → RestartGame()
```

### 8.2 Botão de Menu
```
(Opcional) Adicione botão para Menu Principal
```

---

## Fase 9: Melhorias Visuais (Opcional)

### 9.1 Adicionar Efeitos
```
Sons: Quando bloco é clicado
Partículas: Quando bloco é destruído
Animação: Pinguim equilibrando
```

### 9.2 Indicador de Turno
```
Adicione texto na UI mostrando jogador atual
```

### 9.3 Variáveis Personalizáveis
```
Adicione sliders para:
- Dificuldade (quantos blocos)
- Velocidade física
- Tamanho do tabuleiro
```

---

## ✅ Checklist Final

- [ ] Câmera ortográfica, vendo todo o tabuleiro
- [ ] Layers criadas corretamente
- [ ] Pinguim no centro, com Rigidbody2D
- [ ] Blocos em grid, com IceBlock.cs
- [ ] Bordas em todos os 4 lados
- [ ] GameManager e BoardManager configurados
- [ ] UI funcional (game over, restart)
- [ ] Cliques alternam turnos
- [ ] Blocos isolados caem
- [ ] Pinguim cai ao ficar sem suporte

---

## 🎯 Resultado Esperado

Quando tudo estiver funcionando:

1. **Cena de jogo**: Pinguim em pé sobre blocos, rodeado de bordas
2. **Jogabilidade**: Clique em bloco → muda sprite → após 3x some
3. **Física**: Blocos sem suporte caem naturalmente
4. **Turnos**: Console mostra alternância entre P1 e P2
5. **Game Over**: Quando pinguim cai, painel surge com vencedor
6. **Restart**: Botão reinicia a cena

---

## 💡 Dicas de Debug

```csharp
// Se nada acontece ao clicar:
// → Verifique se o bloco tem BoxCollider2D
// → Verifique se Main Camera existe
// → Verifique se GameController está na cena

// Se blocos não caem:
// → Verifique se Rigidbody2D foi adicionado
// → Verifique se gravity scale > 0
// → Verifique se BorderLayer está correto

// Se turnos não alternando:
// → Verifique se GameController.instance é null
// → Veja console para debug logs
```

---

## 📱 Próximos Passos

1. **Rede Multiplayer**: Integre com Photon ou Mirror para 2 jogadores conectados
2. **Animações**: Adicione rotação/queda para blocos
3. **Som**: Sfx para cliques e game over
4. **Tabuleiros Diferentes**: Vários mapas com formações diferentes
5. **Power-ups**: Itens para recuperar blocos ou efeitos especiais

