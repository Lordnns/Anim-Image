# Exercice Projections

*Objectif:* Appliquer certains effets de perspective à l'aide des matrices de transformation.

**Modifiez ce fichier pour répondre aux questions**

## Généralité

Un composant `CustomMatrices` a été ajouté à la caméra *Exercice/Camera* de la scène principale. Ce composant applique la matrice de projection configurée en paramètre à la caméra (de façon continue, donc possible de la modifier lors de l'exécution). Cette matrice est sauvegardée dans un object scriptable afin de pouvoir les conserver, et il suffit d'y glisser-déposer un objet scriptable du même type pour essayer diverses configurations.

## 1

Configurez la résolution du jeu pour être en ratio 16:9. Lancez le jeu à l'aide de la matrice *PerspectiveMatrix*. Modifiez le ratio pour une configuration 4:3. Que constatez-vous?

Reponse:
    L'image est compressée horizontalement.

Remarquez les valeurs de la diagonale de la matrice. Dupliquez l'objet de matrice et paramétrez manuellement les valeurs pour obtenir un résultat satisfaisant. Quelle matrice avez-vous obtenu?

Reponse:
    [ 1.299      0         0        0       ]
    [ 0          1.732051  0        0       ]
    [ 0          0        -1.0006  -0.60018 ]
    [ 0          0        -1        0       ]

## 2

Faites le même exercice que précédemment, mais à l'aide de la matrice *OrthoMatrix*.

Reponse:
    L'image est aplatie horizontalement

    [ 0.1         0          0           0      ]
    [ 0           0.1        0           0      ]
    [ 0           0         -0.0020006  -1.0006 ]
    [ 0           0          0           1      ]

## 3

En conservant un ratio 16:9, dupliquez et modifiez la matrice *OrthoMatrix* en modifiant les deux valeurs supérieures de la dernière colonne (demeurez dans la plage ±1). Que constatez-vous? Qu'arrive-t-il si vous modifiez la troisième valeur de cette colonne? Qu'en est-il de la dernière valeur?

Reponse:
    Changer les deux premières composantes (par exemple mettre -0.2 et -0.8 à la place de 0 et 0)
    Décale l’image en x et en y (sans déformer la scène, juste un décalage).

    Changer la troisième composante (z)
    Influence le placement en profondeur et modifie le clipping near/far.

    Changer la dernière valeur (1)
    Perturbe la normalisation homogène et peut causer des artefacts visuels.

## 4

Dupliquez et modifiez la matrice *OrthoMatrix*, appliquez les valeurs "-0.2" et "-0.8" aux deux valeurs supérieures de la dernière colonne, et ajoutez les valeurs "-0.01" et "-0.0178" aux deux valeurs supérieures de la troisième colonne. Que constatez-vous?

Reponse:
    Le décalage en x, y « déplace » toute la scène.
    Les ajouts dans la troisième colonne (celle contenant -0.0020006) créent une légère distorsion de profondeur (parallaxe).
    Visuellement, on peut avoir l’impression que certains éléments se déplacent plus vite ou plus lentement selon leur distance, ce qui peut donner un effet de profondeur plus prononcé.

## 5

Désactivez la caméra *Exercice/Camera* et activez la caméra *Exercice/Camera (1)*. Ajoutez un script au projet, à ajouter sur cette dernière caméra, où vous implémenterez un effet de [Travelling Contrarié](https://fr.wikipedia.org/wiki/Travelling_contrari%C3%A9) centré sur le personnage. Il existe plusieurs références sur le web pour des algorithmes (incluant la page anglaise de l'article ci-dessus), mais vous devriez être en mesure de déduire un algorithme à l'aide d'un peu d'algèbre linéaire en analysant l'effet.