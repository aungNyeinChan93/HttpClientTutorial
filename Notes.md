## for Scaffold

# option 1
dotnet ef dbcontext scaffold \
"Server=.;Database=GameStore;Trusted_Connection=True;TrustServerCertificate=True" \
Microsoft.EntityFrameworkCore.SqlServer

# option 2
dotnet ef dbcontext scaffold \
"[Server=.;Database=GameStore;Trusted_Connection=True;TrustServerCertificate=True]" \
Microsoft.EntityFrameworkCore.SqlServer \
--context [GameStoreDbContext] \
--context-dir Data \
--output-dir Models \
--force

# option 3
dotnet ef dbcontext scaffold \
"[Server=DESKTOP-8CJL57G\SQLEXPRESS;Database=GameStore;Trusted_Connection=True;TrustServerCertificate=True]" \
Microsoft.EntityFrameworkCore.SqlServer \
-o Models \
-c [GameStoreDbContext] \
-f

# opotion 4
dotnet ef dbcontext scaffold \
"Server=DESKTOP-8CJL57G\SQLEXPRESS;Database=GameStore;Trusted_Connection=True;TrustServerCertificate=True" \
Microsoft.EntityFrameworkCore.SqlServer \
--project GameStore.Database \
--startup-project GameStore.Api \
--output-dir Models \
--context-dir Context \
--context GameStoreDbContext \
--force

# option 5
dotnet ef dbcontext scaffold `
"Data Source=DESKTOP-8CJL57G\SQLEXPRESS;Initial Catalog=GameStore;User ID=sa;Password=Anc@****;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False" `
Microsoft.EntityFrameworkCore.SqlServer `
-o Models `
-c GameStoreDbContext `
-f

# option 6
dotnet ef dbcontext scaffold `
"connection-string" `
Microsoft.EntityFrameworkCore.SqlServer `
--project GameStore.Database `
--startup-project GameStore.Api `
-o Models `
-c GameStoreDbContext `
-f

dotnet ef dbcontext scaffold \
"Data Source=DESKTOP-8CJL57G\SQLEXPRESS;Initial Catalog=GameStore;User ID=sa;Password=********;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30" \
Microsoft.EntityFrameworkCore.SqlServer \
-o Models \
-c GameStoreDbContext \
-f


dotnet ef dbcontext scaffold `
"connection-string" `
Microsoft.EntityFrameworkCore.SqlServer `
--project GameStore.Database `
--startup-project GameStore.Api `
-o Models `
-c GameStoreDbContext `
-f

dotnet ef dbcontext scaffold `
"Data Source=DESKTOP-8CJL57G\SQLEXPRESS;Initial Catalog=GameStore;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30" `
Microsoft.EntityFrameworkCore.SqlServer `
-o Models `
-c GameStoreDbContext `
-f
