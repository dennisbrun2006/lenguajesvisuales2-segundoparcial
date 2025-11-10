# 🧩 API Clientes – Segundo Parcial Programación II

**Autor:** Dennis Brunaga  

---

## 🚀 Descripción General

Este proyecto fue desarrollado como parte del **segundo examen parcial de la materia Lenguajes Visuales II**, cuyo objetivo principal fue crear una **API RESTful** utilizando **ASP.NET Core 8 Web API** con **Entity Framework Core**, **Swagger UI**, y **publicación en hosting remoto (MonsterASP.net)**.

El sistema permite **gestionar clientes** y **asociarles archivos**, incluyendo **subida de fotos** y **archivos ZIP**, almacenados en el servidor.  
Todos los endpoints fueron documentados mediante **Swagger**, garantizando facilidad de uso y pruebas directas.

---

## 🧠 Funcionalidades Principales

✅ Registro de clientes con datos personales.  
✅ Subida de **foto de perfil (JPG/PNG)** al servidor.  
✅ Subida de **archivo ZIP** asociado a cada cliente.  
✅ Listado general de clientes registrados.  
✅ Persistencia de datos en **SQL Server remoto**.  
✅ Interfaz de prueba de endpoints con **Swagger UI**.  
✅ Publicación en hosting educativo **MonsterASP.net**.

---

## 🧩 Endpoints Principales

| Método | Ruta | Descripción |
|--------|------|-------------|
| **GET** | `/api/Clientes` | Devuelve el listado completo de clientes. |
| **POST** | `/api/Clientes` | Registra un nuevo cliente con foto y archivo ZIP. |
| **DELETE** | `/api/Clientes/{id}` | Elimina un cliente por su ID. |

---

## 🗂️ Estructura del Proyecto

```
📦 Api
 ┣ 📂 Controllers
 ┃ ┗ 📜 ClientesController.cs
 ┣ 📂 Entities
 ┃ ┣ 📜 Cliente.cs
 ┃ ┗ 📜 ArchivoCliente.cs
 ┣ 📂 Infrastructure
 ┃ ┗ 📂 Persistence
 ┃   ┗ 📜 AppDbContext.cs
 ┣ 📂 Middlewares
 ┃ ┗ 📜 ErrorHandlerMiddleware.cs
 ┣ 📂 wwwroot
 ┃ ┣ 📂 fotos
 ┃ ┗ 📂 archivos
 ┣ 📜 appsettings.json
 ┣ 📜 Program.cs
 ┗ 📜 Api.csproj
```

---

## ⚙️ Tecnologías Utilizadas

| Componente | Tecnología |
|-------------|-------------|
| Lenguaje | C# |
| Framework | ASP.NET Core 8 |
| ORM | Entity Framework Core |
| Base de Datos | SQL Server |
| Interfaz de Pruebas | Swagger UI |
| Hosting | MonsterASP.net |
| IDE | Visual Studio 2022 |

---

## 🌐 Publicación en Hosting

El proyecto fue publicado correctamente en **MonsterASP.net**, cumpliendo con el requerimiento de despliegue remoto.  
La documentación Swagger está disponible públicamente en el siguiente enlace:

🔗 **Swagger Online:**  
http://lv2denis.runasp.net/swagger/index.html

> ⚠️ *Nota:* Al usar hosting gratuito, la aplicación puede tardar unos segundos en activarse si no ha tenido tráfico reciente. Esto es un comportamiento normal del servidor (modo “idle”).  

---



## 🧾 Créditos y Agradecimientos

Desarrollado por **Dennis Brunaga**  
Estudiante de **Ingeniería en Informática – Universidad del Norte**


---

## 📎 Enlaces Importantes

- 🌐 **Swagger:** http://lv2denis.runasp.net/swagger/index.html    
- 🧠 **Tecnología Base:** ASP.NET Core 8 + SQL Server
