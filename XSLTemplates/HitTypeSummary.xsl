<?xml version='1.0' encoding='utf-8'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="html"/>

<xsl:template match="/">
	<html><HEAD><TITLE>Character Manager Export</TITLE>
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
		<body>
			<h1>Hit Type Summary</h1>
			<table>
				<tbody>
					<xsl:for-each select="ArrayOfWeaponSummary/WeaponSummary">
						<tr>
							<td class="CELLBORDER" colSpan="5">
								<h2>
									<xsl:value-of select="WeaponName"/>
								</h2>
							</td>
						</tr>
						<tr>
							<td class="cellborder">
								<strong>Hit Type</strong>
							</td>
							<td align="right" class="cellborder">
								<strong>Hits</strong>
							</td>
							<td align="right" class="cellborder">
								<strong>Hit %</strong>
							</td>
							<td align="right" class="cellborder">
								<strong>Dmg</strong>
							</td>
							<td align="right" class="cellborder">
								<strong>Dmg %</strong>
							</td>
						</tr>
						<xsl:for-each select="HitSummary/HitTypeInfo">
							<tr>
								<td class="cellborder">
									<xsl:value-of select="DisplayName"/>
								</td>
								<td align="right" class="cellborder">
									<xsl:value-of select="HitCount"/>
								</td>
								<td align="right" class="cellborder">
									<xsl:value-of select="format-number(number(HitPercentage), '#0')"/>%
								</td>
								<td align="right" class="cellborder">
									<xsl:value-of select="format-number(number(DamageCaused), '#0.0')"/>
								</td>
								<td align="right" class="cellborder">
									<xsl:value-of select="format-number(number(DamagePercentage),'#0')"/>%
								</td>
							</tr>
						</xsl:for-each>
						<tr>
							<td class="NB" colSpan="2">-</td>
						</tr>
					</xsl:for-each>
				</tbody>
			</table>
		</body>
	</html>
</xsl:template>

</xsl:stylesheet><!-- Stylus Studio meta-information - (c)1998-2004. Sonic Software Corporation. All rights reserved.
<metaInformation>
<scenarios ><scenario default="yes" name="Main" userelativepaths="yes" externalpreview="no" url="ws1.xml" htmlbaseurl="" outputurl="" processortype="msxmldotnet" profilemode="0" profiledepth="" profilelength="" urlprofilexml="" commandline="" additionalpath="" additionalclasspath="" postprocessortype="none" postprocesscommandline="" postprocessadditionalpath="" postprocessgeneratedext=""/></scenarios><MapperMetaTag><MapperInfo srcSchemaPathIsRelative="yes" srcSchemaInterpretAsXML="no" destSchemaPath="" destSchemaRoot="" destSchemaPathIsRelative="yes" destSchemaInterpretAsXML="no"/><MapperBlockPosition></MapperBlockPosition></MapperMetaTag>
</metaInformation>
-->