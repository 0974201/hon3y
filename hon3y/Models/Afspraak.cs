﻿using System.ComponentModel.DataAnnotations;
using System;

namespace hon3y.Models
{
    public class Afspraak
    {
        public int AfspraakId { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public int? Telefoonnummer { get; set; }

        public string AfspraakReden { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Datum { get; set; }
    }
}