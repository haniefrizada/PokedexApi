using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokedexApi.Migrations
{
    public partial class UpdatePokemon_sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE UpdatePokemon
                            @PokemonId INT,
                            @PokemonNo NVARCHAR(6),
                            @Name NVARCHAR(50),
                            @Type NVARCHAR(50),
                            @Description NVARCHAR(200)
                        AS
                        BEGIN
                            UPDATE Pokemons
                            SET PokemonNo = @PokemonNo,
                                Name = @Name,
                                Type = @Type,
                                Description = @Description
                            WHERE Id = @PokemonId;
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
