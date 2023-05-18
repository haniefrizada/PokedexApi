using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokedexApi.Migrations
{
    public partial class DeletePokemon_sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE DeletePokemon
                            @PokemonId INT
                        AS
                        BEGIN
                            DELETE FROM Pokemons
                            WHERE Id = @PokemonId;
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
