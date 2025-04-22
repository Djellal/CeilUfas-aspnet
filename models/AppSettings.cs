namespace CeilUfas.models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AppSettings
    {
        public int Id { get; set; }
        
        [MaxLength(200)]
        public string OrganizationName { get; set; } = string.Empty;      
        
        
        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string Tel { get; set; } = string.Empty;
        
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [MaxLength(200)]
        [Url]
        public string Website { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string Facebook { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string Twitter { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string LinkedIn { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string YouTube { get; set; } = string.Empty;
        
        public int? CurrentSessionId { get; set; }
        
        [ForeignKey("CurrentSessionId")]
        public Session? CurrentSession { get; set; }
        
        public bool RegistrationIsOpened { get; set; } = false;
    }