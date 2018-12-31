﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1201:A field should not follow a property", Justification = "<Pending>", Scope = "member", Target = "~F:UsersApp.EF.Repositories.UnitOfWork._usersContext")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1028:Code should not contain trailing whitespace", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.BLL.Services.IUserRepository.GetUserAsync(System.Int32,System.Threading.CancellationToken)~System.Threading.Tasks.Task{UsersApp.EF.Models.User}")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1028:Code should not contain trailing whitespace", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.BLL.Services.IUserRepository.UpdateUserAsync(System.Int32,UsersApp.EF.Models.User,System.Threading.CancellationToken)~System.Threading.Tasks.Task")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1201:A constructor should not follow a property", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Context.UsersContext.#ctor(Microsoft.EntityFrameworkCore.DbContextOptions{UsersApp.EF.Context.UsersContext})")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1822:Member BuildUserModel does not access instance data and can be marked as static (Shared in VisualBasic)", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Context.UsersContext.BuildUserModel(Microsoft.EntityFrameworkCore.ModelBuilder)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1413:Use trailing comma in multi-line initializers", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Migrations.Initial.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1505:An opening brace should not be followed by a blank line.", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Migrations.Update_data_flow.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1508:A closing brace should not be preceded by a blank line.", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Migrations.Update_data_flow.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1505:An opening brace should not be followed by a blank line.", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Migrations.Update_data_flow.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1508:A closing brace should not be preceded by a blank line.", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Migrations.Update_data_flow.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1201:A constructor should not follow a property", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Repositories.BaseRepository`3.#ctor(`2)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1028:Code should not contain trailing whitespace", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Repositories.UnitOfWork.#ctor(UsersApp.BLL.Services.IUserRepository,UsersApp.EF.Context.UsersContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1063:Modify UnitOfWork.Dispose so that it calls Dispose(true), then calls GC.SuppressFinalize on the current object instance ('this' or 'Me' in Visual Basic), and then returns.", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Repositories.UnitOfWork.Dispose")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1816:Change UnitOfWork.Dispose() to call GC.SuppressFinalize(object). This will prevent derived types that introduce a finalizer from needing to re-implement 'IDisposable' to call it.", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Repositories.UnitOfWork.Dispose")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1512:Single-line comments should not be followed by blank line", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Repositories.UserRepository.#ctor(UsersApp.EF.Context.UsersContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1005:Single line comment should begin with a space.", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Repositories.UserRepository.#ctor(UsersApp.EF.Context.UsersContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1128:Put constructor initializers on their own line", Justification = "<Pending>", Scope = "member", Target = "~M:UsersApp.EF.Repositories.UserRepository.#ctor(UsersApp.EF.Context.UsersContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA2227:Change 'Products' to be read-only by removing the property setter.", Justification = "<Pending>", Scope = "member", Target = "~P:UsersApp.EF.Models.User.Products")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1028:Code should not contain trailing whitespace", Justification = "<Pending>", Scope = "type", Target = "~T:UsersApp.EF.Interfaces.IGenericRepository`2")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1400:Element 'IGenericRepository' should declare an access modifier", Justification = "<Pending>", Scope = "type", Target = "~T:UsersApp.EF.Interfaces.IGenericRepository`2")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1707:Remove the underscores from type name UsersApp.EF.Migrations.Update_data_flow.", Justification = "<Pending>", Scope = "type", Target = "~T:UsersApp.EF.Migrations.Update_data_flow")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "SA1028:Code should not contain trailing whitespace", Justification = "<Pending>", Scope = "type", Target = "~T:UsersApp.EF.Repositories.BaseRepository`3")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1063:Provide an overridable implementation of Dispose(bool) on UnitOfWork or mark the type as sealed. A call to Dispose(false) should only clean up native resources. A call to Dispose(true) should clean up both managed and native resources.", Justification = "<Pending>", Scope = "type", Target = "~T:UsersApp.EF.Repositories.UnitOfWork")]