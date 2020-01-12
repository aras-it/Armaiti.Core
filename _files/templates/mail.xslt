<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:template match="/">
    <html>
      <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=yes" />
        <style>
          body { direction: <xsl:value-of select="/MessageDocument/Direction"/>; }
          .messageBody{ direction: <xsl:value-of select="/MessageDocument/Direction"/>; width: 100% }
          .header{ font-size: xx-small; }
          .title{ font-weight: bolder; }
          .description{ font-family: Tahoma; }
          .links{ text-decoration: none; }
          .footer{ font-size: xx-small; }
        </style>
      </head>
      <body>
        <table class="messageBody">
          <tr>
            <td>
              <div class="header">
                <xsl:value-of select="/MessageDocument/Header"/>
              </div>
              <br/>
            </td>
          </tr>
          <tr>
            <td>
              <div class="title">
                <xsl:value-of select="/MessageDocument/Title"/>
              </div>
              <br/>
            </td>
          </tr>
          <tr>
            <td>
              <br/>
              <div class="description">
                <xsl:value-of select="/MessageDocument/Body"/>
              </div>
              <br/>
            </td>
          </tr>
          <tr>
            <td>
              <div class="links">
                <xsl:value-of select="/MessageDocument/Links"/>
              </div>
            </td>
          </tr>
          <tr>
            <td>
              <br/>
              <div class="footer">
                <xsl:value-of select="/MessageDocument/Footer"/>
              </div>
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>