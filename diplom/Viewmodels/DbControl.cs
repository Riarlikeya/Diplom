namespace diplom.Viewmodels
{
    public class DbControl : RgresDbContext
    {
        public void AddDefect(Defect defect)
        {
            Defects.Add(defect);
            SaveChanges();
        }
        public void RemoveUser(User user)
        {
            Users.Remove(user);
            SaveChanges();
        }
        public void AddUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }
        public void AddTalon(Talon talon)
        {
            Talons.Add(talon);
            SaveChanges();
        }
    }
}
