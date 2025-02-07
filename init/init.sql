create database messenger;
create user pguser with password '123456789';

\c messenger

create table if not exists messages (
    id bigserial primary key,
    text varchar(128) not null,
    timestamp timestamp not null default current_timestamp,
    seq_num bigint not null
);

grant usage on schema public to pguser;
grant all privileges on all tables in schema public to pguser;
grant usage, select on all sequences in schema public to pguser;
alter default privileges in schema public grant all privileges on tables to pguser;
alter default privileges in schema public grant usage, select on sequences to pguser;
grant all privileges on database messenger to pguser;