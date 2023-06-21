# Films
ASP.Net WebApp project about films on RazorPage. DbContext with SqlServer. Repository with sakila database - github.com/jOOQ/sakila
</br>
</br>
<h3>Pages</h3>
Website about films with one Page on MenuTab: Films (Index).</br>
Each film that shows on <strong>Index</strong> page wrapped in card.</br>
</br>
<h3>Services</h3>
FilmsService provides us only one method - GetAll.</br>
Services works with repository that have DbContext with Entity Framework connection to SQL Server.</br>
Local DbContext has few DbSet: Films, Actors, Categories, Languages, FilmsActors and FilmsCategories.</br>
SQL Server database tables filled from sakila database - github.com/jOOQ/sakila.</br>
At the start of the site's work one migrations has to be cteated: InitialCreate.</br>
So Entity Framework with DbContext now know how to work with SQL Server "sakila" database.</br>
<strong>GetAll</strong> request in service returns list of films with included lists of categories and actors.</br>
