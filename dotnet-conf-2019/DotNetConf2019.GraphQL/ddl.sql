SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;


CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';

SET default_tablespace = '';
SET default_with_oids = false;

CREATE TABLE public."Authors" (
    "PublicKey" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Name" text
);

CREATE TABLE public."Comments" (
    "PublicKey" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Text" text,
    "PostId" uuid NOT NULL,
    "SubmittedOn" timestamp with time zone NOT NULL
);

CREATE TABLE public."Images" (
    "PublicKey" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Size" integer NOT NULL,
    "Url" text,
    "PostId" uuid NOT NULL
);

CREATE TABLE public."Posts" (
    "PublicKey" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Title" text,
    "Markdown" text,
    "PublishedOn" timestamp with time zone NOT NULL,
    "AuthorId" uuid NOT NULL
);


ALTER TABLE ONLY public."Authors"
    ADD CONSTRAINT "PK_Authors" PRIMARY KEY ("PublicKey");

ALTER TABLE ONLY public."Comments"
    ADD CONSTRAINT "PK_Comments" PRIMARY KEY ("PublicKey");

ALTER TABLE ONLY public."Images"
    ADD CONSTRAINT "PK_Images" PRIMARY KEY ("PublicKey");

ALTER TABLE ONLY public."Posts"
    ADD CONSTRAINT "PK_Posts" PRIMARY KEY ("PublicKey");


