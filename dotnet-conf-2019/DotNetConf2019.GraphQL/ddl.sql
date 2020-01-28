CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE public."Authors" (
	"PublicKey" uuid NOT NULL DEFAULT uuid_generate_v4(),
	"Name" text NULL,
	CONSTRAINT "PK_Authors" PRIMARY KEY ("PublicKey")
);

CREATE TABLE public."Comments" (
	"PublicKey" uuid NOT NULL DEFAULT uuid_generate_v4(),
	"Text" text NULL,
	"PostId" uuid NOT NULL,
	"SubmittedOn" timestamptz NOT NULL,
	CONSTRAINT "PK_Comments" PRIMARY KEY ("PublicKey")
);

CREATE TABLE public."Images" (
	"PublicKey" uuid NOT NULL DEFAULT uuid_generate_v4(),
	"Size" int4 NOT NULL,
	"Url" text NULL,
	"PostId" uuid NOT NULL,
	CONSTRAINT "PK_Images" PRIMARY KEY ("PublicKey")
);

CREATE TABLE public."Posts" (
	"PublicKey" uuid NOT NULL DEFAULT uuid_generate_v4(),
	"Title" text NULL,
	"Markdown" text NULL,
	"PublishedOn" timestamptz NOT NULL,
	"AuthorId" uuid NOT NULL,
	CONSTRAINT "PK_Posts" PRIMARY KEY ("PublicKey")
);