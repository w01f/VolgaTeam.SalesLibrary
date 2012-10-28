<!DOCTYPE html>
<html>
    <head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    </head>
    <body>
        <div data-role='page'> 
            <div data-role='header' data-theme='b'>
                <div class="ui-btn-inner ui-li">
                    <h2 id="ribbon-title">
                    </h2>
                </div>
            </div> 
            <div data-role='content' id ="content">
                <?php echo $content; ?>
            </div> 
            <div data-role="tabbar" data-iconpos="top" data-theme="b" id="ribbon">
                <ul>
                    <li>
                        <a href="#libraries" data-icon="grid" id ="tab-libraries">
                            Libraries
                        </a>
                    </li>
                    <li>
                        <a href="#search" data-icon="search" id ="tab-search">
                            Search
                        </a>
                    </li>
                    <li>
                        <a href="#" data-icon="delete" id="logout">
                            Log Out
                        </a>
                    </li>
                </ul>
            </div>
        </div>        
    </body>
</html>