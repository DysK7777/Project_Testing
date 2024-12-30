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
### Test: GrantAccess_ActiveUserWithinWorkingHours_ReturnsTrue
Deze test controleert of een actieve gebruiker toegang krijgt binnen de werktijden.

Arrange:
De URL http://localhost:3000/data/acces?id=12345&name=%22test%22&active=true&exist=true geeft aan dat de gebruiker met ID "12345" actief is en bestaat.
De huidige tijd is ingesteld op  17:00 uur, wat binnen de werktijden valt.
Act: De GrantAccess-methode wordt aangeroepen met de toegangspas "12345".
Assert: De test controleert of de methode true retourneert, wat betekent dat de gebruiker toegang krijgt.
### When: "the user attempts to access the building"
Deze stap simuleert het scenario waarin de gebruiker probeert toegang te krijgen tot het gebouw.

### Test: GrantAccess_InactiveUser_ThrowsUnauthorizedAccessException
Deze test controleert of een inactieve gebruiker geen toegang krijgt en een UnauthorizedAccessException gooit.

Arrange:
De URL http://localhost:3000/data/acces?id=12345&name=%22test%22&active=false&exist=true geeft aan dat de gebruiker met ID "12345" inactief is, maar wel bestaat.
De huidige tijd is ingesteld op 17:00 uur, wat binnen de werktijden valt.
Act & Assert:
De test probeert toegang te verlenen aan de gebruiker "12345".
Omdat de gebruiker inactief is, wordt een UnauthorizedAccessException gegooid.

### Test: GrantAccess_UnknownAccessCard_ThrowsArgumentException
Deze test controleert of een onbekende toegangspas een ArgumentException veroorzaakt.

Arrange:
De URL http://localhost:3000/data/acces?id=12345&name=%22test%22&active=false&exist=false geeft aan dat de gebruiker met ID "12345" niet bestaat (parameter exist=false).
De huidige tijd is ingesteld op  17:00 uur, wat binnen de werktijden valt.
Act & Assert:
De test probeert toegang te verlenen aan de gebruiker "12345".
Omdat de gebruiker niet bestaat, wordt een ArgumentException gegooid.

### Test: GrantAccess_AccessOutsideWorkingHours_ThrowsUnauthorizedAccessException
Deze test controleert of een gebruiker geen toegang krijgt buiten de werktijden, zelfs als de gebruiker bestaat.

Arrange:
De URL http://localhost:3000/data/acces?id=12345&name=%22test%22&active=false&exist=true geeft aan dat de gebruiker met ID "12345" inactief is, maar wel bestaat.
De huidige tijd is ingesteld op 20:00 uur, wat buiten de werktijden valt.
Act & Assert:
De test probeert toegang te verlenen aan de gebruiker "12345".
Omdat het buiten de werktijden is, wordt een UnauthorizedAccessException gegooid.
