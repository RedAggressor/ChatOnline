using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Host.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "message_sentiment_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "messsage_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Sentiment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentiment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentimentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Sentiment_SentimentId",
                        column: x => x.SentimentId,
                        principalTable: "Sentiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_SentimentId",
                table: "Message",
                column: "SentimentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Sentiment");

            migrationBuilder.DropSequence(
                name: "message_sentiment_hilo");

            migrationBuilder.DropSequence(
                name: "messsage_hilo");
        }
    }
}
