using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bowling.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<long>(type: "int", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar (50)", nullable: false),
                    CaptainID = table.Column<long>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    TourneyID = table.Column<long>(type: "int", nullable: false),
                    TourneyDate = table.Column<byte[]>(type: "date", nullable: true),
                    TourneyLocation = table.Column<string>(type: "nvarchar (50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.TourneyID);
                });

            migrationBuilder.CreateTable(
                name: "ztblBowlerRatings",
                columns: table => new
                {
                    BowlerRating = table.Column<string>(type: "nvarchar (15)", nullable: false),
                    BowlerLowAvg = table.Column<long>(type: "smallint", nullable: true),
                    BowlerHighAvg = table.Column<long>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ztblBowlerRatings", x => x.BowlerRating);
                });

            migrationBuilder.CreateTable(
                name: "ztblSkipLabels",
                columns: table => new
                {
                    LabelCount = table.Column<long>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ztblSkipLabels", x => x.LabelCount);
                });

            migrationBuilder.CreateTable(
                name: "ztblWeeks",
                columns: table => new
                {
                    WeekStart = table.Column<byte[]>(type: "date", nullable: false),
                    WeekEnd = table.Column<byte[]>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ztblWeeks", x => x.WeekStart);
                });

            migrationBuilder.CreateTable(
                name: "Bowlers",
                columns: table => new
                {
                    BowlerID = table.Column<long>(type: "int", nullable: false),
                    BowlerLastName = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    BowlerFirstName = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    BowlerMiddleInit = table.Column<string>(type: "nvarchar (1)", nullable: true),
                    BowlerAddress = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    BowlerCity = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    BowlerState = table.Column<string>(type: "nvarchar (2)", nullable: true),
                    BowlerZip = table.Column<string>(type: "nvarchar (10)", nullable: true),
                    BowlerPhoneNumber = table.Column<string>(type: "nvarchar (14)", nullable: true),
                    TeamID = table.Column<long>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bowlers", x => x.BowlerID);
                    table.ForeignKey(
                        name: "FK_Bowlers_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tourney_Matches",
                columns: table => new
                {
                    MatchID = table.Column<long>(type: "int", nullable: false),
                    TourneyID = table.Column<long>(type: "int", nullable: true, defaultValueSql: "0")
                        .Annotation("Sqlite:Autoincrement", true),
                    Lanes = table.Column<string>(type: "nvarchar (5)", nullable: true),
                    OddLaneTeamID = table.Column<long>(type: "int", nullable: true, defaultValueSql: "0"),
                    EvenLaneTeamID = table.Column<long>(type: "int", nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tourney_Matches", x => x.MatchID);
                    table.ForeignKey(
                        name: "FK_Tourney_Matches_Teams_EvenLaneTeamID",
                        column: x => x.EvenLaneTeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tourney_Matches_Teams_OddLaneTeamID",
                        column: x => x.OddLaneTeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tourney_Matches_Tournaments_TourneyID",
                        column: x => x.TourneyID,
                        principalTable: "Tournaments",
                        principalColumn: "TourneyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bowler_Scores",
                columns: table => new
                {
                    MatchID = table.Column<long>(type: "int", nullable: false),
                    GameNumber = table.Column<long>(type: "smallint", nullable: false),
                    BowlerID = table.Column<long>(type: "int", nullable: false),
                    RawScore = table.Column<long>(type: "smallint", nullable: true, defaultValueSql: "0")
                        .Annotation("Sqlite:Autoincrement", true),
                    HandiCapScore = table.Column<long>(type: "smallint", nullable: true, defaultValueSql: "0")
                        .Annotation("Sqlite:Autoincrement", true),
                    WonGame = table.Column<byte[]>(type: "bit", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bowler_Scores", x => new { x.MatchID, x.GameNumber, x.BowlerID });
                    table.ForeignKey(
                        name: "FK_Bowler_Scores_Bowlers_BowlerID",
                        column: x => x.BowlerID,
                        principalTable: "Bowlers",
                        principalColumn: "BowlerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Match_Games",
                columns: table => new
                {
                    MatchID = table.Column<long>(type: "int", nullable: false),
                    GameNumber = table.Column<long>(type: "smallint", nullable: false),
                    WinningTeamID = table.Column<long>(type: "int", nullable: true, defaultValueSql: "0")
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match_Games", x => new { x.MatchID, x.GameNumber });
                    table.ForeignKey(
                        name: "FK_Match_Games_Tourney_Matches_MatchID",
                        column: x => x.MatchID,
                        principalTable: "Tourney_Matches",
                        principalColumn: "MatchID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "BowlerID",
                table: "Bowler_Scores",
                column: "BowlerID");

            migrationBuilder.CreateIndex(
                name: "MatchGamesBowlerScores",
                table: "Bowler_Scores",
                columns: new[] { "MatchID", "GameNumber" });

            migrationBuilder.CreateIndex(
                name: "BowlerLastName",
                table: "Bowlers",
                column: "BowlerLastName");

            migrationBuilder.CreateIndex(
                name: "BowlersTeamID",
                table: "Bowlers",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "TourneyMatchesMatchGames",
                table: "Match_Games",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "Team1ID",
                table: "Match_Games",
                column: "WinningTeamID");

            migrationBuilder.CreateIndex(
                name: "TeamID",
                table: "Teams",
                column: "TeamID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Tourney_MatchesEven",
                table: "Tourney_Matches",
                column: "EvenLaneTeamID");

            migrationBuilder.CreateIndex(
                name: "TourneyMatchesOdd",
                table: "Tourney_Matches",
                column: "OddLaneTeamID");

            migrationBuilder.CreateIndex(
                name: "TourneyMatchesTourneyID",
                table: "Tourney_Matches",
                column: "TourneyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bowler_Scores");

            migrationBuilder.DropTable(
                name: "Match_Games");

            migrationBuilder.DropTable(
                name: "ztblBowlerRatings");

            migrationBuilder.DropTable(
                name: "ztblSkipLabels");

            migrationBuilder.DropTable(
                name: "ztblWeeks");

            migrationBuilder.DropTable(
                name: "Bowlers");

            migrationBuilder.DropTable(
                name: "Tourney_Matches");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Tournaments");
        }
    }
}
