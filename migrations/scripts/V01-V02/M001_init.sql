CREATE TABLE IF NOT EXISTS public."ZoneAgence"
(
    id uuid NOT NULL,
    "codeZoneAgence" integer NOT NULL,
    "ZoneAgenceAdresse" text COLLATE pg_catalog."default",
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_ZoneAgence" PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public."Agence"
(
    "Id" uuid NOT NULL,
    "CodeAgence" integer NOT NULL,
    "NomAgence" text COLLATE pg_catalog."default",
    "Adresse" text COLLATE pg_catalog."default",
    "ZoneAgenceId" uuid,
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Agence" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Agence_ZoneAgence_ZoneAgenceId" FOREIGN KEY ("ZoneAgenceId")
        REFERENCES public."ZoneAgence" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
);
CREATE TABLE public."GroupeControle"
(
    "Id" uuid NOT NULL,
    "Name" text COLLATE pg_catalog."default",
    "GroupePond" text COLLATE pg_catalog."default",
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_GroupeControle" PRIMARY KEY ("Id")
);
CREATE TABLE public."Controle"
(
    "Id" uuid NOT NULL,
    "Name" text COLLATE pg_catalog."default",
    "CodeAnomalie" integer NOT NULL,
    "CodeControle" integer NOT NULL,
    "GroupeControleId" uuid,
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Controle" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Controle_GroupeControle_GroupeControleId" FOREIGN KEY ("GroupeControleId")
        REFERENCES public."GroupeControle" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
);


CREATE INDEX "IX_Controle_GroupeControleId"
    ON public."Controle" USING btree
    ("GroupeControleId" ASC NULLS LAST)
    TABLESPACE pg_default;

CREATE INDEX IF NOT EXISTS "IX_Agence_ZoneAgenceId"
    ON public."Agence" USING btree
    ("ZoneAgenceId" ASC NULLS LAST);

CREATE TABLE IF NOT EXISTS public."AllPonderation"
(
    "Id" uuid NOT NULL,
    "Ponderation" text COLLATE pg_catalog."default",
    "TotalPoints" integer NOT NULL,
    "ControleId" uuid,
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AllPonderation" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AllPonderation_Controle_ControleId" FOREIGN KEY ("ControleId")
        REFERENCES public."Controle" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
);


-- Index: IX_AllPonderation_ControleId

-- DROP INDEX IF EXISTS public."IX_AllPonderation_ControleId";

CREATE INDEX IF NOT EXISTS "IX_AllPonderation_ControleId"
    ON public."AllPonderation" USING btree
    ("ControleId" ASC NULLS LAST);

CREATE TABLE public."LotPacket"
(
    "Id" uuid NOT NULL,
    "NomPAcket" text COLLATE pg_catalog."default",
    "EtatLotPacket" integer NOT NULL,
    "NombreDoc" integer NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_LotPacket" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."DocBrute"
(
    "Id" uuid NOT NULL,
    "NomDoc" text COLLATE pg_catalog."default",
    "Commentaire" text COLLATE pg_catalog."default",
    "Etat" integer NOT NULL,
    "LotPacketId" uuid,
    "AgenceId" uuid,
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_DocBrute" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_DocBrute_Agence_AgenceId" FOREIGN KEY ("AgenceId")
        REFERENCES public."Agence" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_DocBrute_LotPacket_LotPacketId" FOREIGN KEY ("LotPacketId")
        REFERENCES public."LotPacket" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
);


CREATE INDEX IF NOT EXISTS "IX_DocBrute_AgenceId"
    ON public."DocBrute" USING btree
    ("AgenceId" ASC NULLS LAST);

CREATE INDEX IF NOT EXISTS "IX_DocBrute_LotPacketId"
    ON public."DocBrute" USING btree
    ("LotPacketId" ASC NULLS LAST);

CREATE TABLE public."LotArchive"
(
    "Id" uuid NOT NULL,
    "CodeLotArchive" integer NOT NULL,
    "LotArchiveName" text COLLATE pg_catalog."default",
    "DateFinSaisie" timestamp with time zone NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_LotArchive" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."Operateur"
(
    id uuid NOT NULL,
    nom text COLLATE pg_catalog."default",
    prenom text COLLATE pg_catalog."default",
    login text COLLATE pg_catalog."default",
    password text COLLATE pg_catalog."default",
    "DateRecrutement" timestamp with time zone NOT NULL,
    mail text COLLATE pg_catalog."default",
    "NumTel" text COLLATE pg_catalog."default",
    "Role" text COLLATE pg_catalog."default",
    "Equipe" text COLLATE pg_catalog."default",
    "Discipline" text COLLATE pg_catalog."default",
    "AgenceId" uuid,
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Operateur" PRIMARY KEY (id),
    CONSTRAINT "FK_Operateur_Agence_AgenceId" FOREIGN KEY ("AgenceId")
        REFERENCES public."Agence" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
);

CREATE INDEX IF NOT EXISTS "IX_Operateur_AgenceId"
    ON public."Operateur" USING btree
    ("AgenceId" ASC NULLS LAST);
CREATE TABLE IF NOT EXISTS public."Operation"
(
    "Id" uuid NOT NULL,
    "CodeOperation" integer NOT NULL,
    "OperationName" text COLLATE pg_catalog."default",
    "Ponderation" integer NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Operation" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."Archive"
(
    "Id" uuid NOT NULL,
    "PathArchive" text COLLATE pg_catalog."default",
    "NomDOc" text COLLATE pg_catalog."default",
    "Commentaire" text COLLATE pg_catalog."default",
    "Etat" integer NOT NULL,
    "ValideArchive" integer NOT NULL,
    "DateFinSaisie" timestamp with time zone NOT NULL,
    "AgenceId" uuid,
    "OperateurId" uuid,
    "DocBruteId" uuid,
    "OperationId" uuid,
    "LotArchiveId" uuid,
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Archive" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Archive_Agence_AgenceId" FOREIGN KEY ("AgenceId")
        REFERENCES public."Agence" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_Archive_DocBrute_DocBruteId" FOREIGN KEY ("DocBruteId")
        REFERENCES public."DocBrute" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_Archive_LotArchive_LotArchiveId" FOREIGN KEY ("LotArchiveId")
        REFERENCES public."LotArchive" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_Archive_Operateur_OperateurId" FOREIGN KEY ("OperateurId")
        REFERENCES public."Operateur" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_Archive_Operation_OperationId" FOREIGN KEY ("OperationId")
        REFERENCES public."Operation" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
);

-- Index: IX_Archive_AgenceId

-- DROP INDEX IF EXISTS public."IX_Archive_AgenceId";

CREATE INDEX IF NOT EXISTS "IX_Archive_AgenceId"
    ON public."Archive" USING btree
    ("AgenceId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_Archive_DocBruteId

-- DROP INDEX IF EXISTS public."IX_Archive_DocBruteId";

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Archive_DocBruteId"
    ON public."Archive" USING btree
    ("DocBruteId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_Archive_LotArchiveId

-- DROP INDEX IF EXISTS public."IX_Archive_LotArchiveId";

CREATE INDEX IF NOT EXISTS "IX_Archive_LotArchiveId"
    ON public."Archive" USING btree
    ("LotArchiveId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_Archive_OperateurId

-- DROP INDEX IF EXISTS public."IX_Archive_OperateurId";

CREATE INDEX IF NOT EXISTS "IX_Archive_OperateurId"
    ON public."Archive" USING btree
    ("OperateurId" ASC NULLS LAST);

CREATE INDEX IF NOT EXISTS "IX_Archive_OperationId"
    ON public."Archive" USING btree
    ("OperationId" ASC NULLS LAST);



CREATE TABLE IF NOT EXISTS public."Data"
(
    "Id" uuid NOT NULL,
    "ControleValue" text COLLATE pg_catalog."default",
    "ArchiveId" uuid,
    "ControleId" uuid,
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Data" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Data_Archive_ArchiveId" FOREIGN KEY ("ArchiveId")
        REFERENCES public."Archive" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_Data_Controle_ControleId" FOREIGN KEY ("ControleId")
        REFERENCES public."Controle" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
);




CREATE INDEX IF NOT EXISTS "IX_Data_ArchiveId"
    ON public."Data" USING btree
    ("ArchiveId" ASC NULLS LAST);

CREATE INDEX IF NOT EXISTS "IX_Data_ControleId"
    ON public."Data" USING btree
    ("ControleId" ASC NULLS LAST)
    TABLESPACE pg_default;





CREATE TABLE IF NOT EXISTS public."Typologie"
(
    "Id" uuid NOT NULL,
    "CibleControle" text COLLATE pg_catalog."default",
    "CiblePondaration" text COLLATE pg_catalog."default",
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Typologie" PRIMARY KEY ("Id")
);
CREATE TABLE IF NOT EXISTS public."Ponderate"
(
    "Id" uuid NOT NULL,
    "Nom" text COLLATE pg_catalog."default",
    "Valeur" text COLLATE pg_catalog."default",
    "OperationId" uuid,
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    "TypologiesId" uuid,
    CONSTRAINT "PK_Ponderate" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Ponderate_Operation_OperationId" FOREIGN KEY ("OperationId")
        REFERENCES public."Operation" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_Ponderate_Typologie_TypologiesId" FOREIGN KEY ("TypologiesId")
        REFERENCES public."Typologie" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
);

CREATE INDEX IF NOT EXISTS "IX_Ponderate_OperationId"
    ON public."Ponderate" USING btree
    ("OperationId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_Ponderate_TypologiesId

-- DROP INDEX IF EXISTS public."IX_Ponderate_TypologiesId";

CREATE INDEX IF NOT EXISTS "IX_Ponderate_TypologiesId"
    ON public."Ponderate" USING btree
    ("TypologiesId" ASC NULLS LAST);



-- Table: public.RawDocuments

-- DROP TABLE IF EXISTS public."RawDocuments";

CREATE TABLE IF NOT EXISTS public."RawDocuments"
(
    "Id" uuid NOT NULL,
    "DocumentName" text COLLATE pg_catalog."default",
    "Observation" text COLLATE pg_catalog."default",
    "DocumentType" integer NOT NULL,
    "State" integer NOT NULL,
    "LotId" uuid,
    "DocumentPictureId" uuid,
    "DocumentFieldsId" uuid,
    "CreatedDate" timestamp with time zone NOT NULL,
    "CreatedBy" text COLLATE pg_catalog."default",
    "CreatedById" text COLLATE pg_catalog."default",
    "ModifiedDate" timestamp with time zone,
    "ModifiedBy" text COLLATE pg_catalog."default",
    "ModifiedById" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_RawDocuments" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_RawDocuments_DocumentPictures_DocumentPictureId" FOREIGN KEY ("DocumentPictureId")
        REFERENCES public."DocumentPictures" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_RawDocuments_DocumentsFields_DocumentFieldsId" FOREIGN KEY ("DocumentFieldsId")
        REFERENCES public."DocumentsFields" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_RawDocuments_Lots_LotId" FOREIGN KEY ("LotId")
        REFERENCES public."Lots" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."RawDocuments"
    OWNER to postgres;
-- Index: IX_RawDocuments_DocumentFieldsId

-- DROP INDEX IF EXISTS public."IX_RawDocuments_DocumentFieldsId";

CREATE INDEX IF NOT EXISTS "IX_RawDocuments_DocumentFieldsId"
    ON public."RawDocuments" USING btree
    ("DocumentFieldsId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_RawDocuments_DocumentPictureId

-- DROP INDEX IF EXISTS public."IX_RawDocuments_DocumentPictureId";

CREATE INDEX IF NOT EXISTS "IX_RawDocuments_DocumentPictureId"
    ON public."RawDocuments" USING btree
    ("DocumentPictureId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_RawDocuments_LotId

-- DROP INDEX IF EXISTS public."IX_RawDocuments_LotId";

CREATE INDEX IF NOT EXISTS "IX_RawDocuments_LotId"
    ON public."RawDocuments" USING btree
    ("LotId" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Table: public.DocumentsFields

-- DROP TABLE IF EXISTS public."DocumentsFields";

CREATE TABLE IF NOT EXISTS public."DocumentsFields"
(
    "Id" uuid NOT NULL,
    "FieldNumber" text COLLATE pg_catalog."default",
    "ClientSignature" text COLLATE pg_catalog."default",
    "BankStamp" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_DocumentsFields" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."DocumentsFields"
    OWNER to postgres;


-- Table: public.DocumentPictures

-- DROP TABLE IF EXISTS public."DocumentPictures";

CREATE TABLE IF NOT EXISTS public."DocumentPictures"
(
    "Id" uuid NOT NULL,
    "Url" text COLLATE pg_catalog."default",
    "PublicId" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_DocumentPictures" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."DocumentPictures"
    OWNER to postgres;


-- Table: public.Lots

-- DROP TABLE IF EXISTS public."Lots";

CREATE TABLE IF NOT EXISTS public."Lots"
(
    "Id" uuid NOT NULL,
    "Name" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Lots" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Lots"
    OWNER to postgres;











