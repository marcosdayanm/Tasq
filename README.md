# Tasq
Tasq es una aplicación web integral desarrollada con ASP.NET Core 6.0, diseñada para mejorar la gestión y coordinación de actividades dentro de una empresa. Utiliza Identity Framework para una autenticación y autorización robusta, asegurando un acceso seguro y personalizado para cada usuario. La aplicación se apoya en una base de datos SQLite3, proporcionando un almacenamiento de datos eficiente y escalable. Tasq permite a los usuarios explorar y gestionar sedes, departamentos y tareas, facilitando la colaboración y eficiencia operativa. Su diseño intuitivo y funcionalidades avanzadas lo convierten en una herramienta esencial para la administración y organización empresarial moderna.


## Creación de Cuenta y Registro:

Puedes crear una nueva cuenta en Tasq proporcionando tus detalles personales, incluyendo nombre, fecha de nacimiento, formación profesional, y una foto. También seleccionarás una sede asociada a tu cuenta.
La dirección de correo electrónico que proporciones será tu identificador único para iniciar sesión.


## Cuentas de Administrador
Éstas cuentas son insertadas directamente en la base de datos para tener un diseño más seguro de la app, las cuentas disponibles como administrador son:

### Cuenta 1:
- Usuario: admin@tasq.com
- Contraseña: Admin00*

### Cuenta 2:
- Usuario: marcos@tasq.com
- Contraseña: Admin00*

### Cuenta 3:
- Usuario: santiago@tasq.com
- Contraseña: Admin00*




## Explorar Sedes:
Accede y explora una lista completa de todas las sedes disponibles en la aplicación, viendo detalles importantes como su ubicación y características.

Detalles de la Sede: Selecciona una sede específica para ver más información, incluyendo los departamentos asociados y las tareas pendientes en cada uno.

Gestión de Sedes para Administradores:
- Crear Nuevas Sedes: Si tienes privilegios de administrador, puedes añadir nuevas sedes a la aplicación, proporcionando detalles como nombre, descripción y ubicación.
- Editar Sedes Existentes: Como administrador, también puedes actualizar la información de las sedes ya existentes.
- Eliminar Sedes: Los administradores pueden eliminar sedes que ya no sean necesarias o relevantes.


## Explorar Departamentos:
Puedes ver todos los departamentos disponibles, obteniendo una idea clara de las diferentes áreas operativas dentro de cada sede.

Detalles de Departamentos: Accede a información detallada de cada departamento, incluyendo las tareas específicas que se están llevando a cabo allí.

Funciones para Administradores:

Crear Departamentos: Si eres un administrador, puedes agregar nuevos departamentos, especificando detalles como nombre, descripción y asignándolos a una sede.
Editar Departamentos: Actualiza la información de los departamentos existentes para mantenerlos al día con los cambios en la organización.
Eliminar Departamentos: Los administradores pueden eliminar departamentos que ya no sean necesarios, ayudando a mantener la estructura organizativa limpia y eficiente.


## Explorar Tareas:
Visualiza todas las tareas disponibles, lo que te ofrece una comprensión clara de los proyectos en curso en los distintos departamentos.

Asignar o Desasignar Tareas: Todos los usuarios pueden tomar la responsabilidad de una tarea, lo cual es útil para la gestión eficiente de proyectos y carga de trabajo.

Crear Tareas: Todos los usuarios pueden agregar nuevas tareas, asignándolas a departamentos específicos y estableciendo fechas de entrega, solo los Administradores pueden editarlas o eliminarlas

Editar Tareas: Como administrador, actualiza la información de las tareas existentes, como su descripción, fecha de entrega o asignación a diferentes departamentos o usuarios.

Eliminar Tareas: Los administradores pueden eliminar tareas que ya no son necesarias, manteniendo así la organización y eficiencia en el flujo de trabajo.



## Usuarios:
Como usuario de Tasq, aquí tienes lo que puedes hacer en la sección de usuarios:

Explorar Usuarios: Visualiza una lista de todos los usuarios registrados en la aplicación, accediendo a una visión general de los miembros de la empresa y sus roles.

Detalles del Usuario: Consulta información detallada sobre tu usuario, incluyendo su nombre, correo electrónico, fecha de nacimiento, formación profesional y la sede a la que está asignado, pero además, podrás ver todas las tareas en las que estás inscriuto para gestionarlas de mejor manera





