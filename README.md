
Zanim zacznie się testować kod programu trzeba najpierw ustawić plik docker-compose.yaml jako plik główny repozytorium.

Aby ustawić plik docker-compose.yml jako pliku głównego repozytorium w GitHub:
Użyj opcji --file podczas pushowania:

Uruchom polecenie git push origin HEAD:master --file docker-compose.yml.

Aby wykonać polecenie git push origin HEAD:master --file docker-compose.yml w GitHubie, musisz:

Sklonować repozytorium do lokalnego folderu:

git clone https://github.com/<użytkownik>/<repozytorium>.git
Przejdź do lokalnego folderu repozytorium:

cd <repozytorium>
Dodaj plik docker-compose.yml do repozytorium:

git add docker-compose.yml
Wykonaj polecenie git push z opcją --file:

git push origin HEAD:master --file docker-compose.yml
Uwaga:

Zamień https://github.com/<użytkownik>/<repozytorium>.git na adres URL Twojego repozytorium GitHub.
Upewnij się, że masz zainstalowany Git na swoim komputerze.
Alternatywnie:

Możesz użyć interfejsu GitHub do wysłania pliku docker-compose.yml. W tym celu:

Przejdź do repozytorium w GitHub.
Kliknij przycisk "Ustawienia".
Wybierz kartę "Pliki".
Kliknij przycisk "Dodaj plik".
Wybierz plik docker-compose.yml i kliknij przycisk "Otwórz".
Plik docker-compose.yml zostanie wysłany do repozytorium i będzie używany do uruchamiania aplikacji.
