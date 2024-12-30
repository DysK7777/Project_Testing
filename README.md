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

