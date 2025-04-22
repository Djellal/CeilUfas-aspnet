namespace CeilUfas.models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AppSettings
{
    public int Id { get; set; }
    public string OrganizationName { get; set; }
    public string Address { get; set; }
    public string Tel { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public string Facebook { get; set; }
    public string Twitter { get; set; }
    public string LinkedIn { get; set; }
    public string YouTube { get; set; }
    public bool RegistrationIsOpened { get; set; }
    public string LogoImagePath { get; set; } // New property for logo image
    
    // Add reference to current session
    public int? CurrentSessionId { get; set; }
    
    [ForeignKey("CurrentSessionId")]
    public Session CurrentSession { get; set; }
}