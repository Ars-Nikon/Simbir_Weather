using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    [Table("Events")]
    public class Event
    {

        public int? Id { get; set; }

        [Required(ErrorMessage = "Название мероприятия не может быть пустым")]
        [Display(Name = "Название мероприятия")]
        public string NameEvent { get; set; }

        public string Id_User { get; set; }
        public bool Done { get; set; }

        public bool CancellationStatus { get; set; }

        [Display(Name = "Описание мероприятия")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Город не может быть пустым")]
        [Display(Name = "Город")]
        public string Region { get; set; }

        [Required(ErrorMessage = "Дата прогноза не может быть пустым")]
        [Display(Name = "Дата прогноза")]
        public DateTime? DateEvent { get; set; }

        [Required(ErrorMessage = "Дата отправи не может быть пустым")]
        [Display(Name = "Дата и время отправки уведомления")]
        public DateTime? DateSendMessage { get; set; }
    }
}
