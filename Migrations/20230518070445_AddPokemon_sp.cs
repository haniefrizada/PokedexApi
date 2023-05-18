using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokedexApi.Migrations
{
    public partial class AddPokemon_sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE AddPokemon
                            @PokemonNo NVARCHAR(6),
                            @Name NVARCHAR(50),
                            @Type NVARCHAR(50),
                            @Description NVARCHAR(200)
                        AS
                        BEGIN
                            INSERT INTO Pokemons (PokemonNo, Name, Type, Description)
                            VALUES (@PokemonNo, @Name, @Type, @Description);
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
