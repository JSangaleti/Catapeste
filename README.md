# Catapeste

Protótipo 2D em Godot 4.7 Mono baseado no [mini-GDD](docs/mini-gdd_catapeste.md).

## Executar

Abra `project.godot` no Godot 4.7.2 Mono e execute o projeto com `F6` ou `F5`.

## Controles

- `Espaço`: trava a força e lança o personagem;
- `W` e `S`: controlam a altura durante o voo;
- `R`: inicia outra tentativa na tela de resultado.

## Estrutura

- `assets/`: arte, fontes, música e efeitos sonoros;
- `autoload/`: estado persistente entre tentativas;
- `data/`: dados de balanceamento e melhorias;
- `docs/`: documentação de design;
- `scenes/`: composição das cenas por responsabilidade;
- `scripts/`: comportamento das cenas por responsabilidade.

A cena inicial é `scenes/main/prototype.tscn`. Os elementos visuais atuais são
placeholders desenhados em código e devem ser substituídos conforme a direção
artística for definida.
