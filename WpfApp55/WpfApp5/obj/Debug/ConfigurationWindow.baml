   M S B A M L   `   `   `  ����B  >WpfApp5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp5WpfApp5  5   	   !    WpfApp5.ConfigurationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp5     5   	   -   5          � Title$R  LНастройки подключения к источнику данных��5   	   $	��400��6:   $	��800��6G   .��5	      � 
���/���.+�5
   
   �  � ColumnDefinitions 5      �� 5      �� 5      �� 5       � RowDefinitions 5      �� 5      6"   �� 5      6"   �� 5      6"   5      �� 5   
   $	��400��6   $��15q�6    
����  � �� Padding$ 30q�6>   $��Center=�6K    �� CornerRadius$ 15��6f    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 20Q�6#      Color$ 	#FFA7BBC1��63      ShadowDepth$ 5Q�6E   6W   ._�5      �� .+�5      �� $<��6Меню подключения к базе даных��6   $��20(�6A   $��Medium"�6O   $��	0 0 0 200q�6c   6x   � 5      $��0 0 0 20q�6   .+�5      �� -   5        	 ��Name$	 	btConnect��5   N   $��Center=�6   
��� ���{�.  $8��2Подключить источник данных��6�   $��32��6�   $��266,0q�6�   $	��Top=�6�   �� 5      -   5        $��btGetDataBase��5   L   
��� ���$
��Left=�61   {�.  $(��"Получить список БД��6�   $	��137��6�   6�   �� 5      -   5        $��btCancel��5   M   
��� ���$��Right=�61   $��Отмена��6]   $��0,0,10,0q�6�   6�   �� 5       -   5        $��cbServerList��5       
 �� 
IsEditable
 . {�.  $��53��6W   $��234,-152,0,131q�6c   6   �� 5!      -   5        $��cbDataBaseList��5!      
 .  {�.  $��57��6Z   $��234,-89,0,64q�6f   6~   �� 5"      -   5        $��lblServerList��5"   0   
��� ���$#��Список серверов��6E   $��0��6_   $��20(�6m   $��0,-81,483,54q�6{   �� 5#      -   5        $��lblDataBaseList��5#   0   
��� ���$&�� Список баз данных��6G   $��0,-152,483,121q�6c   $��20(�6{   6�   5$      5%      5&      5'      5(      