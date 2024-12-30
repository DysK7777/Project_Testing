# Project_Testing

## TDD: 
### Test: GrantAccess_ActiveUserWithinWorkingHours_ReturnsTrue

Deze test controleert of een actieve gebruiker toegang krijgt tot het gebouw wanneer het binnen de werktijden is (tussen 9:00 en 17:00).
Arrange: We maken een mock van IAccessInfoProvider en stellen in dat de gebruiker met ID 12345 actief is. Daarnaast stellen we de huidige tijd in op 2024-01-01 10:00, wat binnen werktijden valt.
Act: We proberen toegang te verlenen aan de gebruiker met ID 12345.
Assert: We controleren of de toegang wordt verleend door de waarde true te verwachten.


### Test: GrantAccess_InactiveUser_ThrowsUnauthorizedAccessException

Deze test controleert of een inactieve gebruiker geen toegang krijgt tot het gebouw.
Arrange: We maken een mock van IAccessInfoProvider en stellen in dat de gebruiker met ID 67890 inactief is. De huidige tijd wordt ingesteld op 2024-01-01 10:00 (binnen werktijd).
Act: We proberen toegang te verlenen aan de gebruiker met ID 67890.
Assert: We verwachten een UnauthorizedAccessException, omdat de gebruiker inactief is.


### Test: GrantAccess_UnknownAccessCard_ThrowsArgumentException

Deze test controleert of er een fout optreedt wanneer een onbekende toegangspas wordt gebruikt.
Arrange: We maken een mock van IAccessInfoProvider. De huidige tijd wordt ingesteld op 2024-01-01 10:00 (binnen werktijden).
Act: We proberen toegang te verlenen met een onbekende toegangspas 99999.
Assert: We verwachten een ArgumentException, omdat de toegangspas onbekend is.


### Test: GrantAccess_AccessOutsideWorkingHours_ThrowsUnauthorizedAccessException

Deze test controleert of een actieve gebruiker geen toegang krijgt buiten de werktijden (bijvoorbeeld na 17:00).
Arrange: We maken een mock van IAccessInfoProvider en stellen in dat de gebruiker met ID 12345 actief is. De huidige tijd wordt ingesteld op 2024-01-01 20:00, wat buiten werktijd is.
Act: We proberen toegang te verlenen aan de gebruiker met ID 12345 buiten de werktijden.
Assert: We verwachten een UnauthorizedAccessException, omdat de gebruiker buiten de werktijden probeert in te loggen.

## Integratie Test
### Test Setup (TestInitialize)
De TestInitialize methode wordt uitgevoerd voor elke test en zorgt ervoor dat de objecten en mocks correct worden geïnitialiseerd voordat de tests beginnen. In dit geval wordt een mock van IDateTimeProvider en een nieuwe instantie van AccessInfoProvider aangemaakt voor elke test.

### Test: GrantAccess_ActiveUserWithinWorkingHours_ReturnsTrue

Deze test controleert of een actieve gebruiker toegang krijgt tot het gebouw binnen werktijd (bijvoorbeeld van 9:00 tot 17:00).
De mock van IDateTimeProvider wordt ingesteld om de huidige tijd te simuleren als 2024-01-01 10:00 uur, wat binnen werktijden valt.

### Test: GrantAccess_InactiveUser_ThrowsUnauthorizedAccessException

Deze test controleert of een inactieve gebruiker geen toegang krijgt, zelfs als het binnen werktijd is.
De mock van IDateTimeProvider is weer ingesteld op de tijd binnen werktijd (2024-01-01 10:00 uur), maar de gebruiker ID ("67890") is inactief.

### Test: GrantAccess_UnknownAccessCard_ThrowsArgumentException

Deze test controleert wat er gebeurt als een onbekende toegangspas wordt gebruikt om toegang te krijgen.
De mock IDateTimeProvider is ingesteld op de tijd binnen werktijd (2024-01-01 10:00 uur), maar de toegangspas ID ("99999") is onbekend.

### Test: GrantAccess_AccessOutsideWorkingHours_ThrowsUnauthorizedAccessException

Deze test verifieert dat een actieve gebruiker geen toegang krijgt als de toegang buiten werktijd wordt geprobeerd (bijvoorbeeld na 17:00).
De mock van IDateTimeProvider is ingesteld op de tijd 2024-01-01 20:00 uur, wat buiten werktijd valt.

## AcceptieTesten Gherkin
### Given: "the user "(.*)" is active"
Deze stap configureert de mock IAccessInfoProvider zodat het systeem een actieve gebruiker met het opgegeven toegangspas ID kan herkennen.

### Given: "the user "(.*)" is inactive"
Deze stap configureert de mock IAccessInfoProvider zodat het systeem een inactieve gebruiker met het opgegeven toegangspas ID kan herkennen.

### Given: "the user "(.*)" is unknown"
Deze stap configureert de mock IAccessInfoProvider zodat het systeem een onbekende gebruiker met het opgegeven toegangspas ID niet kan herkennen.

### Given: "the current time is (.*)"
Deze stap configureert de mock IDateTimeProvider zodat het systeem de huidige tijd kan simuleren. De tijd wordt ingesteld op de opgegeven tijd.

### When: "the user attempts to access the building"
Deze stap simuleert het scenario waarin de gebruiker probeert toegang te krijgen tot het gebouw.

### Then: "access should be granted"
Deze stap controleert of de toegang daadwerkelijk is verleend aan de gebruiker.

### Then: "access should be denied"
Deze stap controleert of de toegang geweigerd is aan de gebruiker.
