using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kdega.ScormEngine.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitScormEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CmiCores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: true),
                    LearnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LessonLocation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Credit = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    LessonStatus = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Entry = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    ScoreChildren = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScoreRaw = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ScoreMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ScoreMax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LessonMode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    Exit = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    SessionTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CompletionStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CompletionThreshold = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgressMeasure = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ScaledPassingScore = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ScoreScaled = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SuccessStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LearnerScormPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmiCores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogEvent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestUri = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScormPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleFromManifest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleFromUpload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndexPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageFolderPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScormVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManifestIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManifestXmlBase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManifestVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManifestMetadataSchema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManifestMetadataSchemaVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManifestMetadataLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultOrganizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceXmlBase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScormPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CmiCommentFromLearners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: true),
                    N = table.Column<int>(type: "int", nullable: true),
                    NComment = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    NLocation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NTimestamp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CmiCoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmiCommentFromLearners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmiCommentFromLearners_CmiCores_CmiCoreId",
                        column: x => x.CmiCoreId,
                        principalTable: "CmiCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CmiCommentFromLms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: true),
                    N = table.Column<int>(type: "int", nullable: true),
                    NComment = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    NLocation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NTimestamp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ScoIdentifier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CmiCoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmiCommentFromLms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmiCommentFromLms_CmiCores_CmiCoreId",
                        column: x => x.CmiCoreId,
                        principalTable: "CmiCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CmiInteractions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: true),
                    N = table.Column<int>(type: "int", nullable: true),
                    NId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    InteractionTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Weighting = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StudentResponse = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: true),
                    Result = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Latency = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    MsreplTranVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Timestamp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CmiCoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmiInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmiInteractions_CmiCores_CmiCoreId",
                        column: x => x.CmiCoreId,
                        principalTable: "CmiCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CmiLearnerPreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AudioLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AudioCaptioning = table.Column<int>(type: "int", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeliverySpeed = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Text = table.Column<int>(type: "int", nullable: true),
                    MsreplTranVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CmiCoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmiLearnerPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmiLearnerPreferences_CmiCores_CmiCoreId",
                        column: x => x.CmiCoreId,
                        principalTable: "CmiCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CmiObjectives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: true),
                    N = table.Column<int>(type: "int", nullable: true),
                    NId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NScoreRaw = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NScoreMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NScoreMax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NScoreScaled = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NSuccessStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NCompletionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NProgressMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MsreplTranVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CmiCoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmiObjectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmiObjectives_CmiCores_CmiCoreId",
                        column: x => x.CmiCoreId,
                        principalTable: "CmiCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScormSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LearnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDatetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDatetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScoIdentifier = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CmiCoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScormSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScormSessions_CmiCores_CmiCoreId",
                        column: x => x.CmiCoreId,
                        principalTable: "CmiCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CmiData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LearnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LearnerName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    SuspendData = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    LaunchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasteryScore = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxTimeAllowed = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    TimeLimitAction = table.Column<string>(type: "nvarchar(19)", maxLength: 19, nullable: true),
                    TotalTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AdlNavRequest = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AdlNavRequestValidContinue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AdlNavRequestValidPrevious = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AdlNavRequestValidChoice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CmiComments = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    CmiCommentsFromLms = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: true),
                    ScoIdentifier = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ScormContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScormPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CmiCoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmiData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmiData_CmiCores_CmiCoreId",
                        column: x => x.CmiCoreId,
                        principalTable: "CmiCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CmiData_ScormPackages_ScormPackageId",
                        column: x => x.ScormPackageId,
                        principalTable: "ScormPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnerScormPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LearnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatePassed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastOpenedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ScormPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CmiCoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnerScormPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnerScormPackages_CmiCores_CmiCoreId",
                        column: x => x.CmiCoreId,
                        principalTable: "CmiCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearnerScormPackages_ScormPackages_ScormPackageId",
                        column: x => x.ScormPackageId,
                        principalTable: "ScormPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Structure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdlseqObjectivesGlobalToSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImsssSequencing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetadataLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScormPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_ScormPackages_ScormPackageId",
                        column: x => x.ScormPackageId,
                        principalTable: "ScormPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Href = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XmlBase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdlcpScormType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScormPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_ScormPackages_ScormPackageId",
                        column: x => x.ScormPackageId,
                        principalTable: "ScormPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CmiInteractionsCorrectResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    N = table.Column<int>(type: "int", nullable: true),
                    Pattern = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: true),
                    InteractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmiInteractionsCorrectResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmiInteractionsCorrectResponses_CmiInteractions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "CmiInteractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CmiInteractionsObjectives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    N = table.Column<int>(type: "int", nullable: true),
                    InteractionN = table.Column<int>(type: "int", nullable: true),
                    ObjectiveId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MsreplTranVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InteractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmiInteractionsObjectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmiInteractionsObjectives_CmiInteractions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "CmiInteractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentifierRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: true),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdlcpTimeLimitAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdlcpDataFromLms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdlcpCompletionThresholds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImsssSequencing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdlnavPresentation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationItems_OrganizationItems_ParentItemId",
                        column: x => x.ParentItemId,
                        principalTable: "OrganizationItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationItems_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResourceDependencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentifierRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceDependencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceDependencies_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Href = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceFiles_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CmiCommentFromLearners_CmiCoreId",
                table: "CmiCommentFromLearners",
                column: "CmiCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CmiCommentFromLms_CmiCoreId",
                table: "CmiCommentFromLms",
                column: "CmiCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CmiData_CmiCoreId",
                table: "CmiData",
                column: "CmiCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CmiData_ScormPackageId",
                table: "CmiData",
                column: "ScormPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_CmiInteractions_CmiCoreId",
                table: "CmiInteractions",
                column: "CmiCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CmiInteractionsCorrectResponses_InteractionId",
                table: "CmiInteractionsCorrectResponses",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_CmiInteractionsObjectives_InteractionId",
                table: "CmiInteractionsObjectives",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_CmiLearnerPreferences_CmiCoreId",
                table: "CmiLearnerPreferences",
                column: "CmiCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CmiObjectives_CmiCoreId",
                table: "CmiObjectives",
                column: "CmiCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnerScormPackages_CmiCoreId",
                table: "LearnerScormPackages",
                column: "CmiCoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearnerScormPackages_ScormPackageId",
                table: "LearnerScormPackages",
                column: "ScormPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationItems_OrganizationId",
                table: "OrganizationItems",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationItems_ParentItemId",
                table: "OrganizationItems",
                column: "ParentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ScormPackageId",
                table: "Organizations",
                column: "ScormPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceDependencies_ResourceId",
                table: "ResourceDependencies",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceFiles_ResourceId",
                table: "ResourceFiles",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ScormPackageId",
                table: "Resources",
                column: "ScormPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ScormSessions_CmiCoreId",
                table: "ScormSessions",
                column: "CmiCoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CmiCommentFromLearners");

            migrationBuilder.DropTable(
                name: "CmiCommentFromLms");

            migrationBuilder.DropTable(
                name: "CmiData");

            migrationBuilder.DropTable(
                name: "CmiInteractionsCorrectResponses");

            migrationBuilder.DropTable(
                name: "CmiInteractionsObjectives");

            migrationBuilder.DropTable(
                name: "CmiLearnerPreferences");

            migrationBuilder.DropTable(
                name: "CmiObjectives");

            migrationBuilder.DropTable(
                name: "EventLogs");

            migrationBuilder.DropTable(
                name: "LearnerScormPackages");

            migrationBuilder.DropTable(
                name: "OrganizationItems");

            migrationBuilder.DropTable(
                name: "ResourceDependencies");

            migrationBuilder.DropTable(
                name: "ResourceFiles");

            migrationBuilder.DropTable(
                name: "ScormSessions");

            migrationBuilder.DropTable(
                name: "CmiInteractions");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "CmiCores");

            migrationBuilder.DropTable(
                name: "ScormPackages");
        }
    }
}
