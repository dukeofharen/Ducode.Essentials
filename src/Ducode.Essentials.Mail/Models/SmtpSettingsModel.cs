namespace Ducode.Essentials.Mail.Models
{
   /// <summary>
   /// A class for holding the SMTP settings.
   /// </summary>
   public class SmtpSettingsModel
   {
      /// <summary>
      /// Gets or sets the host.
      /// </summary>
      /// <value>
      /// The host.
      /// </value>
      public string Host { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [use default credentials].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [use default credentials]; otherwise, <c>false</c>.
      /// </value>
      public bool UseDefaultCredentials { get; set; }

      /// <summary>
      /// Gets or sets the username.
      /// </summary>
      /// <value>
      /// The username.
      /// </value>
      public string Username { get; set; }

      /// <summary>
      /// Gets or sets the password.
      /// </summary>
      /// <value>
      /// The password.
      /// </value>
      public string Password { get; set; }

      /// <summary>
      /// Gets or sets the port.
      /// </summary>
      /// <value>
      /// The port.
      /// </value>
      public int Port { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [enable SSL].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [enable SSL]; otherwise, <c>false</c>.
      /// </value>
      public bool EnableSsl { get; set; }
   }
}
