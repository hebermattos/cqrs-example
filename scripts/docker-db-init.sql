USE [master]
GO

IF DB_ID('products') IS NOT NULL
  set noexec on 

CREATE DATABASE [products];
GO