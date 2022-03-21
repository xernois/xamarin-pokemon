# Projet dévelopment mobile ( Xamarin.forms )

## Introduction

Dans le cadre du cours M4104C (développement mobile), nous avions à réaliser une applications sous Xamarin.forms avec l'api `PokeApi`. Cette dernière devait contenir certaines fonctionnalités essentielles tel que :

- une page d'accueil
- une page permettant l'affichage d'un liste de pokemon
- une page d'ajout
- une page de détails
- stockage des pokemons dans une base de données sur le téléphone
- respecter le pattern `MVVM` (Model View ViewModel)

Pour commencer notre réflexion, nous avons commencé à faire quelques maquettes que nous avons plus ou moins bien implémenté.

### Page d'accueil:
<img src="./readme_assets/Page d'accueil.png" width="250px"/>

### Page liste
<img src="./readme_assets/Page liste.png" width="250px"/>

### Page détail:
<img src="./readme_assets/Page détail Bulbizarre.png" width="250px"/>

### Page d'ajout
<div style="display: flex; flex-direction: row; gap: 1rem;">
    <img src="./readme_assets/Page ajout [vide].png" width="250px"/>
    <img src="./readme_assets/Page ajout [plein].png" width="250px"/>
</div>

## Notre travail

Nous avons réussi à implémenter un certain nombre de fonctionnalités. Nous avons d'abord réussi à implémenter toutes les fonctionnalités requise pour ce projet. Mais nous avons également pu intégrer d'autres fonctionnalités à notre application :

- une barre de recherche dans la page de la liste
- la possibilité de supprimer un pokemon
- également de le modifier

Pour ce qui en est de la partie visuel, nous avons essayé de faire une application fidèle à nos maquettes, malheureusement nous avons était limité par notre manque de connaissance avec ce framwork. Notamment pour ce qui est composant et leur personnalisation, nous avons privilégié les composants natifs même si ils n'ont pas forcement le visuel recherché.

## Axe d'amélioration

Plusieurs axes de notre application pourraient être améliorés ou même ajoutés, par exemple :

- la gestion des types et un petit peu bancale, les picker ne sont pas basés sur les valeurs du Dictionnaire, ce qui implique que pour ajouter un type, il faut modifier le xaml de la page d'ajout ainsi que le fichier `Constants.cs`.

- Une fonctionnalité permettant de mettre des pokemons en favoris aurait elle aussi pu voir le jour.

- Un système de filtre ou de trie sur la recherche dans la liste peut être implémenté

- La police aurait pu être modifié.


## Nous

<div style="display:flex; flex-direction:row; gap: .5rem;">
<a href="https://github.com/lianki36"><img src="https://avatars.githubusercontent.com/u/81920218?v=4" width="150px"/></a>

<a href="https://github.com/xernois"><img src="https://avatars.githubusercontent.com/u/32645608?v=4" width="150px"/></a>
</div>
