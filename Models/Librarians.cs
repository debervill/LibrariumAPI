﻿namespace LibrariumAPI.Models
{
    public class Librarians
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public bool isAdmin { get; set; }

    }
}
