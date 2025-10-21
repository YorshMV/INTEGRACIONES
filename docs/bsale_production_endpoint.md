# Bsale Integración: Obtención de la URL productiva

Para mover la integración desde el ambiente de pruebas al ambiente productivo es necesario resolver la URL base a la que se deben enviar las consultas. Bsale expone un **servicio de credenciales** pensado precisamente para este fin. El flujo recomendado por soporte consiste en los siguientes pasos:

1. Identifica la credencial (hash) que Bsale entregó al crear la instancia que vas a integrar. Generalmente viene en la sección "API Keys" del portal de partners o en el correo de activación.
2. Realiza una llamada HTTPS al endpoint de credenciales reemplazando `<hash>` por la credencial asignada:

   ```bash
   curl -H "Accept: application/json" \
        https://credential.bsale.io/v1/instances/basic/<hash>.json
   ```

   - Este endpoint es diferente al usado en los entornos sandbox; si el hash es válido la respuesta incluirá los datos de conexión definitivos.
   - Asegúrate de invocarlo desde una red con salida a Internet y de almacenar el hash de forma segura: es la clave para acceder a la configuración de la empresa.
3. En la respuesta JSON busca el atributo `url`. Dicho atributo corresponde al dominio productivo que debe utilizar la integración en sus llamadas posteriores (por ejemplo, `https://<subdominio>.bsale.cloud`).
4. Actualiza la configuración de la integración (variables de entorno, archivos `.env`, parámetros en SAP, etc.) para que la URL base de los servicios REST apunte al dominio obtenido en el paso anterior.
5. Conserva el resto de parámetros (client id/secret, credenciales HTTP Basic, etc.) tal como fueron entregados para el ambiente productivo.

## Referencias

- Ejemplo entregado por soporte Bsale: `https://credential.bsale.io/v1/instances/basic/7166db62a7847a91ed0857aa1b205a93ac363ece.json`.
- Documentación oficial para integradores: <https://docs.bsale.dev/>.

> **Nota:** El endpoint de credenciales únicamente expone metadatos de conexión. Las llamadas a recursos de negocio (boletas, facturas, productos, etc.) deben realizarse contra el dominio retornado en el campo `url` usando los endpoints documentados en la API pública.
