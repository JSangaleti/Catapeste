# Catapeste
## Mini Game Design Document

## 1. Visão geral

**Título:** Catapeste  
**Gênero:** Arcade / Casual / Side-scroller 2D  
**Engine:** Godot 4  
**Perspectiva:** Visão lateral 2D  
**Plataforma:** Desktop

### Conceito

**Catapeste** é um jogo casual 2D baseado em habilidade e progressão no qual o jogador utiliza uma catapulta para lançar um plebeu doente em direção a uma fortaleza inimiga.

Cada tentativa é composta por dois momentos principais de habilidade:

1. determinar a força do lançamento através de um minigame de precisão;
2. controlar verticalmente o personagem durante o voo para desviar de obstáculos.

A distância alcançada durante cada lançamento gera uma recompensa convertida em dinheiro. Esse dinheiro pode ser utilizado para melhorar a catapulta e permitir lançamentos cada vez mais longos.

O objetivo final do jogo é conseguir lançar o personagem para dentro da fortaleza inimiga.

---

## 2. Ambientação e enredo

O jogo possui ambientação medieval fictícia com caráter humorístico.

Dois povos fictícios estão em guerra. Durante o cerco de uma cidade fortificada, o exército atacante decide utilizar uma estratégia incomum: lançar plebeus contaminados pela peste para dentro da fortaleza inimiga utilizando uma catapulta.

O jogador participa das sucessivas tentativas de lançamento e melhora progressivamente a catapulta até possuir alcance suficiente para atingir a fortaleza.

A ambientação é inspirada livremente em relatos históricos de utilização de cadáveres ou pessoas contaminadas durante cercos, mas o jogo não representa povos, cidades ou conflitos históricos reais.

Os nomes das cidades, povos e personagens serão fictícios e definidos posteriormente.

---

## 3. Objetivo do jogador

### Objetivo de cada tentativa

Percorrer a maior distância possível durante o lançamento.

Para isso, o jogador deve:

- conseguir uma boa força no minigame de lançamento;
- controlar o personagem durante o voo;
- desviar dos obstáculos presentes no percurso;
- manter-se em voo pelo maior tempo e distância possíveis.

### Objetivo geral

Melhorar progressivamente a catapulta até conseguir lançar um plebeu para dentro da fortaleza inimiga.

Ao alcançar a fortaleza, o objetivo principal do jogo é concluído.

---

## 4. Core loop

O ciclo principal de gameplay será:

**Definir força → Lançar → Controlar o personagem → Desviar de obstáculos → Percorrer distância → Terminar tentativa → Receber dinheiro → Melhorar catapulta → Novo lançamento**

Representação simplificada:

```text
Minigame de força
        ↓
    Lançamento
        ↓
       Voo
        ↓
Desviar de obstáculos
        ↓
  Fim da tentativa
        ↓
 Calcular distância
        ↓
     Recompensa
        ↓
 Melhorar catapulta
        ↓
   Novo lançamento
```

---

## 5. Minigame de lançamento

Antes de cada tentativa, o jogador determina a potência do lançamento através de um minigame de precisão.

Será apresentado um medidor em formato semicircular dividido em regiões correspondentes à força do lançamento:

- Fraco;
- Médio;
- Forte;
- Máximo.

Um indicador se movimentará continuamente pelo medidor e o jogador deverá pressionar o comando de lançamento no momento desejado.

As regiões correspondentes às forças maiores serão menores e, consequentemente, mais difíceis de acertar.

Dessa forma, atingir a potência máxima exigirá maior precisão do jogador.

### Resultado do minigame

A região atingida determina a potência inicial do lançamento.

Quanto melhor o resultado:

- maior a velocidade inicial;
- maior o potencial de distância do lançamento.

O jogador **não controla o ângulo da catapulta**. O ângulo de lançamento será definido pelo próprio jogo.

---

## 6. Lançamento

Após a definição da potência, o personagem é automaticamente lançado pela catapulta.

A força obtida no minigame influencia diretamente sua velocidade inicial e sua capacidade potencial de alcançar maiores distâncias.

Depois do lançamento, inicia-se a segunda etapa de habilidade da tentativa: o controle durante o voo.

---

## 7. Controle durante o voo

Durante o voo, o personagem se desloca continuamente da esquerda para a direita.

O jogador possui controle sobre sua posição vertical.

Os comandos permitem:

- movimentar o personagem para cima;
- movimentar o personagem para baixo.

O controle vertical deverá ser utilizado para desviar dos obstáculos encontrados pelo caminho.

O jogador não controla diretamente o deslocamento horizontal durante o voo.

A implementação e o balanceamento exatos da movimentação vertical serão definidos durante a prototipação.

---

## 8. Obstáculos

Os obstáculos são o principal desafio durante a etapa de voo.

Eles serão divididos inicialmente em três categorias.

### 8.1. Obstáculos aéreos

Pássaros aparecerão em diferentes posições verticais durante o percurso.

Sua posição poderá variar, obrigando o jogador a movimentar o personagem para cima ou para baixo para evitá-los.

### 8.2. Obstáculos terrestres

Obstáculos surgirão a partir da parte inferior do cenário e possuirão alturas variáveis.

Os elementos inicialmente previstos são:

- árvores;
- construções;
- atalaias.

Como partem do chão, esses obstáculos exigirão principalmente que o jogador consiga ganhar ou manter altura suficiente para ultrapassá-los.

### 8.3. Projéteis inimigos

Também poderão existir projéteis atravessando a trajetória do personagem.

Os tipos inicialmente previstos são:

- flechas;
- bolas de canhão.

Esses elementos representam as tentativas dos defensores da região inimiga de impedir que o personagem alcance a fortaleza.

---

## 9. Geração e disposição dos obstáculos

Parte dos obstáculos poderá aparecer em posições variáveis durante as tentativas.

Pássaros poderão ocupar diferentes posições verticais.

Árvores, construções e atalaias poderão possuir diferentes alturas.

A geração deverá respeitar a jogabilidade, evitando combinações que tornem o desvio inevitavelmente impossível.

Os detalhes do sistema de geração e o nível de aleatoriedade serão definidos durante o desenvolvimento.

---

## 10. Distância

A principal medida de desempenho de uma tentativa será a distância percorrida.

A distância será contabilizada desde o ponto inicial de lançamento até o ponto final alcançado pelo personagem.

Durante o voo, a interface deverá informar ao jogador a distância atualmente percorrida.

Ao final da tentativa, será apresentada a distância total alcançada.

---

## 11. Recompensa e dinheiro

A distância percorrida determinará a recompensa da tentativa.

Quanto maior a distância alcançada, maior será a quantidade de pontos recebidos.

Esses pontos serão convertidos em dinheiro utilizado na progressão do jogo.

A fórmula exata de:

- pontuação;
- conversão em dinheiro;
- valores das recompensas;

será definida posteriormente durante o balanceamento.

---

## 12. Sistema de progressão

O dinheiro obtido nas tentativas será utilizado para melhorar a catapulta.

As melhorias deverão aumentar progressivamente a capacidade do jogador de alcançar distâncias maiores.

A melhoria da catapulta constitui o principal mecanismo de progressão do jogo.

Os atributos específicos que poderão ser melhorados, seus níveis, custos e efeitos ainda serão definidos durante o desenvolvimento.

O princípio geral será:

**jogar → conseguir dinheiro → melhorar a catapulta → conseguir ir mais longe.**

---

## 13. Estrutura do percurso

O cenário será apresentado lateralmente e acompanhará o avanço horizontal do personagem.

O percurso começa na região onde está localizada a catapulta e termina na fortaleza inimiga.

Durante o caminho existirão:

- elementos naturais;
- árvores;
- pássaros;
- construções;
- atalaias;
- defesas inimigas;
- projéteis.

A composição visual exata das regiões intermediárias ainda será definida.

---

## 14. Fortaleza inimiga

A fortaleza representa o objetivo final do jogo.

Ela estará localizada a uma distância suficientemente grande para que não seja possível alcançá-la com a catapulta inicial.

O jogador precisará realizar sucessivas tentativas, acumular dinheiro e melhorar a catapulta até possuir alcance suficiente.

A distância exata da fortaleza será determinada durante o balanceamento do jogo.

---

## 15. Condição de fim da tentativa

A tentativa termina quando o personagem não consegue mais continuar avançando pelo percurso.

Nesse momento serão calculados:

- distância alcançada;
- pontuação;
- recompensa em dinheiro.

Após o resultado, o jogador poderá utilizar seu dinheiro em melhorias e iniciar uma nova tentativa.

As regras específicas para determinar quando o personagem deixa definitivamente de avançar serão refinadas durante a implementação da física do jogo.

---

## 16. Condição de vitória

O jogador vence quando consegue realizar um lançamento que alcance a fortaleza inimiga e faça o personagem entrar em seu interior.

Essa é a meta final da progressão do jogo.

---

## 17. Interface

### Antes do lançamento

A interface deverá apresentar o medidor semicircular de força contendo as regiões:

- Fraco;
- Médio;
- Forte;
- Máximo.

Também deverá apresentar o indicador móvel utilizado pelo jogador para definir a potência.

### Durante o voo

A interface deverá apresentar pelo menos:

- distância atualmente percorrida.

Outros elementos poderão ser adicionados posteriormente caso sejam necessários.

### Após a tentativa

A interface deverá apresentar pelo menos:

- distância alcançada;
- recompensa obtida;
- opção de acessar as melhorias;
- opção de iniciar uma nova tentativa.

---

## 18. Direção artística

O jogo terá apresentação visual em 2D.

A direção artística deverá ser compatível com:

- ambientação medieval fictícia;
- natureza casual do jogo;
- tom humorístico da premissa.

O estilo gráfico específico dos personagens, cenários, interface e animações ainda será definido.

---

## 19. Áudio

O jogo deverá possuir música e efeitos sonoros adequados às principais ações.

Os recursos de áudio específicos ainda serão definidos durante o desenvolvimento.

Entre as ações que poderão possuir efeitos estão:

- funcionamento da catapulta;
- lançamento;
- voo;
- colisões;
- interação com obstáculos;
- resultado da tentativa.

---

## 20. Escopo atual

O escopo atualmente definido para **Catapeste** contém:

- jogo 2D de visão lateral;
- uma catapulta;
- personagem humano utilizado como projétil;
- minigame semicircular de força;
- quatro regiões de potência;
- lançamento com ângulo não controlado pelo jogador;
- física de lançamento;
- controle vertical durante o voo;
- deslocamento horizontal durante o voo;
- cálculo da distância;
- pássaros como obstáculos aéreos;
- árvores como obstáculos terrestres;
- construções como obstáculos terrestres;
- atalaias como obstáculos terrestres;
- flechas como projéteis;
- bolas de canhão como projéteis;
- recompensa baseada na distância;
- conversão da recompensa em dinheiro;
- sistema de melhoria da catapulta;
- progressão baseada em tentativas sucessivas;
- fortaleza inimiga como destino final;
- condição de vitória ao alcançar a fortaleza;
- interface necessária para lançamento, voo e resultado;
- recursos gráficos e sonoros necessários para a versão final.

---

## 21. Elementos ainda a definir

Os seguintes elementos propositalmente ainda não fazem parte de uma especificação fechada:

- nomes dos povos fictícios;
- nome da cidade ou fortaleza;
- nomes de personagens;
- aparência dos personagens;
- estilo artístico definitivo;
- distância da fortaleza;
- valores de pontuação;
- fórmula de conversão de distância em dinheiro;
- preços das melhorias;
- atributos específicos da catapulta que poderão ser melhorados;
- quantidade e frequência de obstáculos;
- velocidade e comportamento dos projéteis;
- comportamento exato das colisões;
- balanceamento do controle vertical;
- funcionamento preciso da física;
- músicas;
- efeitos sonoros;
- telas e menus além dos necessários para o core loop.

Esses elementos serão definidos e adicionados ao documento conforme o desenvolvimento e a prototipação avancem.

---

## 22. Resumo do gameplay

Em **Catapeste**, o jogador tenta lançar um plebeu contaminado pela peste até uma fortaleza inimiga.

Primeiro, deve acertar a maior força possível em um medidor de precisão. Depois do lançamento, controla o personagem verticalmente enquanto ele avança pelo cenário, desviando de pássaros, árvores, construções, atalaias, flechas e bolas de canhão.

Ao final de cada tentativa, a distância percorrida gera uma recompensa em dinheiro. O jogador utiliza esse dinheiro para melhorar sua catapulta e realizar lançamentos cada vez mais longos.

O ciclo se repete até que o jogador consiga alcançar a fortaleza inimiga.