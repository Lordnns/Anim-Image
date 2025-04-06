# Exercice Sprites

*Objectif:* Implémenter une animation de sprites 2D

## Généralité

Pour chaque sous-exercice, faîtes les ajustements nécessaires au code déjà existant.

## 1

*Objectif:* Interpréter des méta-données générées depuis un outil

En modifiant la méthode `SpriteAnimation::Update()`, ajuster les coordonnées UV des sprites afin qu'elles utilisent les bonnes parties de la feuille de sprite. La zone à afficher, en nombre de pixels, est spécifiée dans la propriété `frame` de la variable `frameDesc`. Vous aurez sans doute besoin de la taille de la texture au complet, disponible dans la variable `spritesheetTexture`, afin de normaliser les valeurs entre 0 et 1, en nombres flottants.

## 2

*Objectif:* Interpréter d'autres méta-données générées depuis un outil

Les dimensions des sprites ne semblent pas correctes. En utilisant la propriété `sourceSize` de la variable `frameDesc`, et en utilisant les informations du pivot stockées dans la propriété du même nom, ajuster la sprite afin qu'elle ait la bonne taille et qu'elle soit centrée correctement selon le pivot. Utilisez les propriétés `sizeDelta` et `pivot` du membre `rectTransform`.

## 3

*Objectif:* Implémenter une animation de sprite

Afin d'ajouter un peu de vie au démo, implémenter le changement de sprite selon le temps. Lorsqu'une sprite est affichée pour plus de temps que ce qui est précisé par la variable `frameLength`, passer à un autre frame. Assurez-vous de vérifier si ce frame existe effectivement. Autrement, revenir au premier frame.
