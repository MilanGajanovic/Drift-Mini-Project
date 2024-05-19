namespace Drifters.Domain.Entities
{
    public class Driver
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Nationality { get; private set; }
        public int ChampionshipsWon { get; private set; }

        public Driver(string name, string nationality, int championshipsWon)
        {
            SetName(name);
            SetNationality(nationality);
            SetChampionshipsWon(championshipsWon);
        }

        public Driver()
        {

        }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.");
            Name = name;
        }

        public void SetNationality(string nationality)
        {
            if (string.IsNullOrWhiteSpace(nationality))
                throw new ArgumentException("Nationality cannot be null or empty.");
            Nationality = nationality;
        }

        public void SetChampionshipsWon(int championshipsWon)
        {
            if (championshipsWon < 0)
                throw new ArgumentException("Championships won cannot be negative.");
            ChampionshipsWon = championshipsWon;
        }
    }
}