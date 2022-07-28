Naam: MOhamad Alamoroush
Studentnummer: 483618


Uitleg Sprint 2: 

In S2 folder zijn de volgende files te vinden: Analyse document, Testplan en Testmartix, 
images van de Class diagram en de ERD.

Isotita: 

3 lagen stractuur toegevoegd. Werkende codes zijn voraal bij de Job (Daar werkt alles behalve de select functie). 

De cshtml pagina en de crud functies voor Job werken allemaal (behalve index). 
In de JobController worden de gegevens tussen de view en de logic heen en terug gestuurd.

In de logic layer is er JobLogic te vinden die gegevens gebruikt wat van de presentatie layer komt en met behulp van de gemaakte models (Models Folder) in de logic layer,
wordt er verder verwezen naar de Dal layer.

In de Dal Layer heb ik de volgende stractuur (Als ik het goed begrijp van Onno moeten de naamgevingen toch anders zijn)

Functions folder: Hier komen de data van de Logic layer
JobFunctions (Dat staat gelijk aan JobDal maar andere naamgeving).

Hier in worden functies met queries gemaakt.Die queries worden gestuurd naar functies in de DatabaseContext. Daar worden de quries uitgevoerd en dan gaat resultaat de andere kant op.


Sprint 3: 

Aanpassingen op documentatie en hoe de app reageert op bepaalde situatie.
CRUD bij CompanyController gemaakt.

Sprint 5:

Documentatie aangepast en database query's document toegeveogd om advance query's te gebruiken. 
Toelechting toegevoegd in mijn ontwerpdocument en ik heb keuzes verantwoordt.
Mijn applicatie komt nu overheen met mijn class-diagram en hij voledt aan must-eisen van het requierments tabel. 
Database validaties ook gebruikt in ViewModels in mijn applicatie.

In mijn applicatie:
1- Gebruik gemaakt van encapsulation en ik gebruik nu objecten i.p.v. fk-id.
2- Code is meer leesbaar zoals gesproken in een feedback
3- Ik gebruik ViewModels in mijn presentatie-laag om database validatie vast te leggen en velden types en wat kan daar ingevuld worden.
4- Ik gebruik PresLogicDTO met doel om geen data in te vullen in de presentatie-laag maar eerste naar de logica-laag sturen en daar worden dan proprties van een model ingevuld.
5- Ik gebruik DTOConvertor die data naar object-class converteert of van een object-class naar object-DTO-class om die sturen naar de dal-laag voor alerlei acties.
6- Ik maak niet meer een object aan en dan vul ik elke proprtie in maar door een feedback weet ik een beter manier om gelijk de object volledig aan te maken.
7- Uit een feedback heb ik fout weggehaald wat ik maakte die niet goed komt met OOP-principe. Namelijk een id sturen naar een methode waar een object is al aangemaakt dus de id isal bekend.
8- Unit-test: af in IsotitaTest
