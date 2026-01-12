# VideoGameApp

## 📌 Περιγραφή
Το VideoGameApp είναι μία full-stack web εφαρμογή που αναπτύχθηκε στο πλαίσιο της Τελικής Εργασίας του Coding Factory.
Η εφαρμογή επιτρέπει τη διαχείριση βιντεοπαιχνιδιών (CRUD λειτουργίες) και χρησιμοποιεί βάση δεδομένων SQLite.

Ο χρήστης μπορεί να:
- Προβάλει τη λίστα παιχνιδιών
- Δημιουργήσει νέο παιχνίδι
- Επεξεργαστεί υπάρχον παιχνίδι
- Διαγράψει παιχνίδι
- Δει λεπτομέρειες παιχνιδιού

---

## 🛠️ Τεχνολογίες
- ASP.NET Core
- Razor Pages
- Entity Framework Core
- SQLite
- Bootstrap
- Swagger (Development mode)

---

## 🧱 Domain Model
Η εφαρμογή βασίζεται στα παρακάτω entities:
- **Game**
- **Genre**
- **Studio**

Υπάρχουν σχέσεις μεταξύ των entities (Foreign Keys & Navigation Properties).

---

## 🗄️ Βάση Δεδομένων
Η εφαρμογή χρησιμοποιεί SQLite.
Κατά την εκκίνηση:
- Εφαρμόζονται αυτόματα τα migrations
- Δημιουργείται η βάση δεδομένων αν δεν υπάρχει
- Εισάγονται αρχικά δεδομένα (seed data)

---

## ▶️ Build & Run (Τοπικά)

### Προαπαιτούμενα
- .NET SDK 7.0 ή νεότερο
- SQLite (ενσωματωμένο μέσω EF Core)

### Βήματα
```bash
git clone https://github.com/Tselep/VideoGameApp.git
cd VideoGameApp
dotnet restore
dotnet run
