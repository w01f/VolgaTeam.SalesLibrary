<?php
class Utils
{
    public static function parseFont($fontString)
    {
        $font = new Font();
        $font->name = 'Arial';
        $font->size = 12;
        $font->isBold = FALSE;
        $font->isItalic = FALSE;

        //determine style
        if (preg_match('/style=(.*)/', $fontString, $matches))
        {
            $stylePart = strtolower($matches[1]);
            if (strpos($stylePart, 'bold') !== false)
                $font->isBold = TRUE;
            if (strpos($stylePart, 'italic') !== false)
                $font->isItalic = TRUE;

            $fontString = str_replace(', ' . $matches[0], '', $fontString);
        }

        $parts = explode(',', $fontString);
        if (isset($parts))
        {
            //determine name
            $font->name = trim($parts[0]);
            //determine size
            $font->size = str_replace('px', '', str_replace('pt', '', $parts[1]));
        }
        return $font;
    }

    public static function getAvailableFormats($extension)
    {
        switch ($extension)
        {
            case 'ppt':
            case 'pptx':
                $formats[] = 'ppt';
                $formats[] = 'pdf';
                $formats[] = 'png';
                $formats[] = 'jpeg';
                break;
            case 'doc':
            case 'docx':
                $formats[] = 'doc';
                $formats[] = 'pdf';
                $formats[] = 'png';
                $formats[] = 'jpeg';
                break;
            case 'xls':
            case 'xlsx':
                $formats[] = 'xls';
                break;
            case 'pdf':
                $formats[] = 'pdf';
                $formats[] = 'png';
                $formats[] = 'jpeg';
                break;
            case 'mpeg':
            case 'wmv':
            case 'avi':
            case 'wmz':
            case 'mpg':
            case 'asf':
            case 'mov':
            case 'mp4':
            case 'm4v':
            case 'flv':
            case 'ogv':
            case 'ogm':
            case 'ogx':
                $formats[] = 'mp4';
                break;
            case 'png':
                $formats[] = 'png';
                break;            
            case 'jpg':
            case 'jpeg':                
                $formats[] = 'jpeg';
                break;                        
            case 'url':
                $formats[] = 'url';
                break;            
        }
        if (isset($formats))
            return $formats;
    }

}

?>
