<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
<HTML><HEAD><TITLE>Character Manager Export</TITLE>
<STYLE>BODY {
	FONT-SIZE: 10px; BACKGROUND: #ffffff; COLOR: #000000; FONT-FAMILY: Verdana, Arial, sans-serif
}
H1 {
	FONT-SIZE: 12px; BACKGROUND: #ffffff; COLOR: #000000; FONT-FAMILY: Verdana, Arial, sans-serif
}
H2 {
	FONT-SIZE: 12px; BACKGROUND: #ffffff; COLOR: #000000; FONT-FAMILY: Verdana, Arial, sans-serif
}
.CELLBORDER {
	FONT-SIZE: 10px; BORDER-LEFT-COLOR: #b6b7cb; BACKGROUND: #ffffff; COLOR: #000000; BORDER-TOP-COLOR: #b6b7cb; BORDER-BOTTOM: #b6b7cb 1px solid; FONT-FAMILY: Verdana, Arial, sans-serif; BORDER-RIGHT-COLOR: #b6b7cb
}

NB {
	FONT-SIZE: 10px; FONT-FAMILY: Verdana, Arial, sans-serif; BORDER-ALL: #ffffff 0px solid;
}
</STYLE>
</HEAD>

<BODY>
<H1>Weapon Data Report</H1>
<TABLE>
  <TBODY>
<xsl:for-each select="ArrayOfWeaponSummary/WeaponSummary">
  <TR>
    <TD COLSPAN="3" class="CELLBORDER"><H2><xsl:value-of select="WeaponName"/></H2></TD>
  </TR>
  <TR>
  	<TD class="cellborder">Fired</TD>
    <TD class="cellborder"><xsl:value-of select="ShotsFired"/></TD>
  </TR>
  <TR>
  	<TD class="cellborder">Shots Hit</TD>
    <TD class="cellborder"><xsl:value-of select="ShotsHit"/> (<xsl:value-of select="format-number(number(PercentageShotsHit) * 10, '#0.00')"/>%)</TD>
  </TR>
  <TR>
  	<TD class="cellborder">Shots Missed</TD>
    <TD class="cellborder"><xsl:value-of select="ShotsMissed"/> (<xsl:value-of select="format-number(number(PercentageShotsMissed) * 10, '#0.00')"/>%)</TD>
  </TR>
  <TR>
  	<TD class="cellborder">Total Damage</TD>
    <TD class="cellborder"><xsl:value-of select="format-number(TotalDamage, '##.00')"/></TD>
  </TR>
  <TR>
  	<TD class="cellborder">Average Damage</TD>
    <TD class="cellborder"><xsl:value-of select="format-number(AverageDamage, '##.00')"/></TD>
  </TR>

  <TR>
  <TD COLSPAN="2" class="NB">-</TD>
  </TR>

</xsl:for-each>
</TBODY>
</TABLE>
</BODY>
</HTML>	
</xsl:template>

</xsl:stylesheet><!-- Stylus Studio meta-information - (c)1998-2004. Sonic Software Corporation. All rights reserved.
<metaInformation>
<scenarios ><scenario default="yes" name="Main" userelativepaths="yes" externalpreview="no" url="ws.xml" htmlbaseurl="" outputurl="" processortype="msxmldotnet" profilemode="0" urlprofilexml="" commandline="" additionalpath="" additionalclasspath="" postprocessortype="none" postprocesscommandline="" postprocessadditionalpath="" postprocessgeneratedext=""/></scenarios><MapperInfo srcSchemaPathIsRelative="yes" srcSchemaInterpretAsXML="no" destSchemaPath="" destSchemaRoot="" destSchemaPathIsRelative="yes" destSchemaInterpretAsXML="no"/>
</metaInformation>
-->