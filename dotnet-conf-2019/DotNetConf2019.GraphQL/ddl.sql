--
-- PostgreSQL database dump
--

-- Dumped from database version 11.5
-- Dumped by pg_dump version 11.5

-- Started on 2020-01-28 11:32:20 MST

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

--
-- TOC entry 2 (class 3079 OID 29077)
-- Name: uuid-ossp; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;


--
-- TOC entry 3234 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION "uuid-ossp"; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';


SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 209 (class 1259 OID 29173)
-- Name: Authors; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Authors" (
    "PublicKey" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Name" text
);


ALTER TABLE public."Authors" OWNER TO postgres;

--
-- TOC entry 199 (class 1259 OID 20985)
-- Name: OrigAuthors; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OrigAuthors" (
    "Id" integer NOT NULL,
    "Name" text
);


ALTER TABLE public."OrigAuthors" OWNER TO postgres;

--
-- TOC entry 198 (class 1259 OID 20983)
-- Name: Authors_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."OrigAuthors" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Authors_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 207 (class 1259 OID 29132)
-- Name: Comments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Comments" (
    "PublicKey" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Text" text,
    "PostId" uuid NOT NULL,
    "SubmittedOn" timestamp with time zone NOT NULL
);


ALTER TABLE public."Comments" OWNER TO postgres;

--
-- TOC entry 201 (class 1259 OID 20995)
-- Name: OrigComments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OrigComments" (
    "Id" integer NOT NULL,
    "Text" text,
    "PostId" integer NOT NULL,
    "SubmittedOn" timestamp with time zone NOT NULL
);


ALTER TABLE public."OrigComments" OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 20993)
-- Name: Comments_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."OrigComments" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Comments_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 208 (class 1259 OID 29141)
-- Name: Images; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Images" (
    "PublicKey" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Size" integer NOT NULL,
    "Url" text,
    "PostId" uuid NOT NULL
);


ALTER TABLE public."Images" OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 21005)
-- Name: OrigImages; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OrigImages" (
    "Id" integer NOT NULL,
    "Size" integer NOT NULL,
    "Url" text,
    "PostId" integer NOT NULL
);


ALTER TABLE public."OrigImages" OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 21003)
-- Name: Images_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."OrigImages" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Images_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 205 (class 1259 OID 21015)
-- Name: OrigPosts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OrigPosts" (
    "Id" integer NOT NULL,
    "AuthorId" integer NOT NULL,
    "Title" text,
    "Markdown" text,
    "PublishedOn" timestamp with time zone NOT NULL
);


ALTER TABLE public."OrigPosts" OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 29104)
-- Name: Posts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Posts" (
    "PublicKey" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Title" text,
    "Markdown" text,
    "PublishedOn" timestamp with time zone NOT NULL,
    "AuthorId" uuid NOT NULL
);


ALTER TABLE public."Posts" OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 21013)
-- Name: Posts_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."OrigPosts" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Posts_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 197 (class 1259 OID 20978)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 3107 (class 2606 OID 29181)
-- Name: Authors PK_Authors; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Authors"
    ADD CONSTRAINT "PK_Authors" PRIMARY KEY ("PublicKey");


--
-- TOC entry 3103 (class 2606 OID 29140)
-- Name: Comments PK_Comments; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Comments"
    ADD CONSTRAINT "PK_Comments" PRIMARY KEY ("PublicKey");


--
-- TOC entry 3105 (class 2606 OID 29149)
-- Name: Images PK_Images; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Images"
    ADD CONSTRAINT "PK_Images" PRIMARY KEY ("PublicKey");


--
-- TOC entry 3101 (class 2606 OID 29151)
-- Name: Posts PK_Posts; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Posts"
    ADD CONSTRAINT "PK_Posts" PRIMARY KEY ("PublicKey");


--
-- TOC entry 3099 (class 2606 OID 20982)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


-- Completed on 2020-01-28 11:32:20 MST

--
-- PostgreSQL database dump complete
--
