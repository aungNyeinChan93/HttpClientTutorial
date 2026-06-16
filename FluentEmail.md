## Configuration
builder.Services.AddFluentEmail(email)
    .AddSmtpSender("smtp.gmail.com", 587, email, emailAppPassword);

