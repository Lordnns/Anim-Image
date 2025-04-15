# Exercice Anim Maillage

*Objectif:* Implémenter une animation vectorielle

## Généralité

Pour chaque sous-exercice, faîtes les ajustements nécessaires au code déjà existant.

## 1

*Objectif:* Appliquer une transformation de maillage depuis un squelette

Le fichier `TentacleMeshGenerator.cs` génère un maillage et un squelette destiné à être manipulé par un vertex shader. Le fichier `TentacleSkeleton.cs` fait les calculs appropriés afin de convertir correctement les rotations et translations en leurs matrices constituantes, passées ensuites au vertex shader. Modifier le fichier `Skinned.shader` afin d'implémenter les déformation de maillage à l'aide du vertex shader. Le gros du travail est fait, il ne reste qu'à appliquer les matrices de transformation pondérées au modèle. Vous pourrez vérifier la déformation du modèle à l'aide des propriétés ```rotations``` du composant `TentacleAnimation` de l'entité.

Le modèle et son squelette est fait de façon très brute et certaines zones de transitions peuvent former des pointes, ignorer ce détail. Également, vous aurez besoin de l'opérateur de transtypage `int` afin d'utiliser les paramètres ```float2``` en tant qu'index de tableau.

## 2

*Objectif:* Animer le squelette à l'aide d'une animation cinématique

Créer une animation par *key frames* du maillage en définissant votre propre structure de données, et l'exécuter. Vous pouvez vous inspirer des animations de l'exercice d'animations de sprites pour vos structures. On vous demande d'éviter d'utiliser les composants d'animation déjà intégrés à Unity.

## 3

*Objectif:* Implémenter une stratégie de cinématique inverse

Faire suivre le curseur par l'extrémité du modèle. Vous pouvez trouver des exemples d'utilisation du curseur dans la démo de transformations de coordonnées (première semaine). Bonne chance!

Il est important de se limiter aux stratégies fonctionnant en 2D afin de simplifier le problème.
