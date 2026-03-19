namespace PieceUsur
{
    partial class PieceUsur
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Title = new Label();
            font = new Label();
            label1 = new Label();
            Date_text = new Label();
            OF_Text = new Label();
            OP_Text = new Label();
            Article_Text = new Label();
            CDC_Text = new Label();
            Date_textBox = new TextBox();
            OF_textBox = new TextBox();
            OP_textBox = new TextBox();
            Article_textBox = new TextBox();
            CDC_textBox = new TextBox();
            label2 = new Label();
            Piece_dataGridView = new DataGridView();
            Valider_Piece = new Button();
            Operateur_name_text = new Label();
            User_Text = new Label();
            Machine_Label = new Label();
            Machine_Texbox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)Piece_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // Title
            // 
            Title.AutoSize = true;
            Title.BackColor = SystemColors.ActiveCaption;
            Title.Font = new Font("Britannic Bold", 40.2F, FontStyle.Regular, GraphicsUnit.Point);
            Title.Location = new Point(-1, 0);
            Title.Name = "Title";
            Title.Size = new Size(415, 74);
            Title.TabIndex = 1;
            Title.Text = "Pièces Usure";
            // 
            // font
            // 
            font.AutoSize = true;
            font.BackColor = SystemColors.ActiveCaption;
            font.Font = new Font("Segoe UI", 70F, FontStyle.Regular, GraphicsUnit.Point);
            font.ForeColor = SystemColors.ActiveCaption;
            font.Location = new Point(408, 0);
            font.Name = "font";
            font.Size = new Size(765, 155);
            font.TabIndex = 2;
            font.Text = "Pieces Usures";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ControlLight;
            label1.Font = new Font("Segoe UI", 60F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLight;
            label1.Location = new Point(-1, 74);
            label1.Name = "label1";
            label1.Size = new Size(1141, 133);
            label1.TabIndex = 3;
            label1.Text = "Piece usur label 1 label 2";
            // 
            // Date_text
            // 
            Date_text.AutoSize = true;
            Date_text.BackColor = SystemColors.ControlLight;
            Date_text.Location = new Point(34, 95);
            Date_text.Name = "Date_text";
            Date_text.Size = new Size(48, 20);
            Date_text.TabIndex = 4;
            Date_text.Text = "Date :";
            // 
            // OF_Text
            // 
            OF_Text.AutoSize = true;
            OF_Text.BackColor = SystemColors.ControlLight;
            OF_Text.Location = new Point(34, 147);
            OF_Text.Name = "OF_Text";
            OF_Text.Size = new Size(38, 20);
            OF_Text.TabIndex = 5;
            OF_Text.Text = "OF : ";
            // 
            // OP_Text
            // 
            OP_Text.AutoSize = true;
            OP_Text.BackColor = SystemColors.ControlLight;
            OP_Text.Location = new Point(306, 95);
            OP_Text.Name = "OP_Text";
            OP_Text.Size = new Size(35, 20);
            OP_Text.TabIndex = 6;
            OP_Text.Text = "OP :";
            // 
            // Article_Text
            // 
            Article_Text.AutoSize = true;
            Article_Text.BackColor = SystemColors.ControlLight;
            Article_Text.Location = new Point(306, 147);
            Article_Text.Name = "Article_Text";
            Article_Text.Size = new Size(59, 20);
            Article_Text.TabIndex = 7;
            Article_Text.Text = "Article :";
            // 
            // CDC_Text
            // 
            CDC_Text.AutoSize = true;
            CDC_Text.BackColor = SystemColors.ControlLight;
            CDC_Text.Location = new Point(617, 99);
            CDC_Text.Name = "CDC_Text";
            CDC_Text.Size = new Size(45, 20);
            CDC_Text.TabIndex = 8;
            CDC_Text.Text = "CDC :";
            // 
            // Date_textBox
            // 
            Date_textBox.BackColor = SystemColors.ControlLight;
            Date_textBox.Location = new Point(88, 92);
            Date_textBox.Name = "Date_textBox";
            Date_textBox.Size = new Size(177, 27);
            Date_textBox.TabIndex = 10;
            // 
            // OF_textBox
            // 
            OF_textBox.BackColor = SystemColors.ControlLight;
            OF_textBox.Location = new Point(88, 143);
            OF_textBox.Name = "OF_textBox";
            OF_textBox.Size = new Size(177, 27);
            OF_textBox.TabIndex = 11;
            // 
            // OP_textBox
            // 
            OP_textBox.BackColor = SystemColors.ControlLight;
            OP_textBox.Location = new Point(380, 92);
            OP_textBox.Name = "OP_textBox";
            OP_textBox.Size = new Size(190, 27);
            OP_textBox.TabIndex = 12;
            // 
            // Article_textBox
            // 
            Article_textBox.BackColor = SystemColors.ControlLight;
            Article_textBox.Location = new Point(380, 144);
            Article_textBox.Name = "Article_textBox";
            Article_textBox.Size = new Size(190, 27);
            Article_textBox.TabIndex = 13;
            // 
            // CDC_textBox
            // 
            CDC_textBox.BackColor = SystemColors.ControlLight;
            CDC_textBox.Location = new Point(720, 88);
            CDC_textBox.Name = "CDC_textBox";
            CDC_textBox.Size = new Size(175, 27);
            CDC_textBox.TabIndex = 15;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(25, 219);
            label2.Name = "label2";
            label2.Size = new Size(359, 35);
            label2.TabIndex = 16;
            label2.Text = "Sélectionner les pièces d’usure:";
            // 
            // Piece_dataGridView
            // 
            Piece_dataGridView.BackgroundColor = SystemColors.ControlLight;
            Piece_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Piece_dataGridView.Location = new Point(165, 287);
            Piece_dataGridView.Name = "Piece_dataGridView";
            Piece_dataGridView.RowHeadersWidth = 51;
            Piece_dataGridView.RowTemplate.Height = 29;
            Piece_dataGridView.Size = new Size(786, 279);
            Piece_dataGridView.TabIndex = 17;
            // 
            // Valider_Piece
            // 
            Valider_Piece.BackColor = Color.MediumAquamarine;
            Valider_Piece.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            Valider_Piece.Location = new Point(776, 581);
            Valider_Piece.Name = "Valider_Piece";
            Valider_Piece.Size = new Size(175, 46);
            Valider_Piece.TabIndex = 18;
            Valider_Piece.Text = "Valider";
            Valider_Piece.UseVisualStyleBackColor = false;
            Valider_Piece.Click += button1_Click;
            // 
            // Operateur_name_text
            // 
            Operateur_name_text.AutoSize = true;
            Operateur_name_text.BackColor = SystemColors.ActiveCaption;
            Operateur_name_text.Location = new Point(776, 28);
            Operateur_name_text.Name = "Operateur_name_text";
            Operateur_name_text.Size = new Size(0, 20);
            Operateur_name_text.TabIndex = 19;
            // 
            // User_Text
            // 
            User_Text.AutoSize = true;
            User_Text.BackColor = SystemColors.ActiveCaption;
            User_Text.Location = new Point(776, 28);
            User_Text.Name = "User_Text";
            User_Text.Size = new Size(81, 20);
            User_Text.TabIndex = 20;
            User_Text.Text = "User_name";
            // 
            // Machine_Label
            // 
            Machine_Label.AutoSize = true;
            Machine_Label.BackColor = SystemColors.ControlLight;
            Machine_Label.Location = new Point(617, 147);
            Machine_Label.Name = "Machine_Label";
            Machine_Label.Size = new Size(72, 20);
            Machine_Label.TabIndex = 21;
            Machine_Label.Text = "Machine: ";
            // 
            // Machine_Texbox
            // 
            Machine_Texbox.BackColor = SystemColors.ControlLight;
            Machine_Texbox.Location = new Point(720, 143);
            Machine_Texbox.Name = "Machine_Texbox";
            Machine_Texbox.Size = new Size(175, 27);
            Machine_Texbox.TabIndex = 22;
            // 
            // PieceUsur
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1095, 639);
            Controls.Add(Machine_Texbox);
            Controls.Add(Machine_Label);
            Controls.Add(User_Text);
            Controls.Add(Operateur_name_text);
            Controls.Add(Valider_Piece);
            Controls.Add(Piece_dataGridView);
            Controls.Add(label2);
            Controls.Add(CDC_textBox);
            Controls.Add(Article_textBox);
            Controls.Add(OP_textBox);
            Controls.Add(OF_textBox);
            Controls.Add(Date_textBox);
            Controls.Add(CDC_Text);
            Controls.Add(Article_Text);
            Controls.Add(OP_Text);
            Controls.Add(OF_Text);
            Controls.Add(Date_text);
            Controls.Add(label1);
            Controls.Add(font);
            Controls.Add(Title);
            Name = "PieceUsur";
            Text = "Form1";
            Load += PieceUsur_Load;
            ((System.ComponentModel.ISupportInitialize)Piece_dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label Title;
        private Label font;
        private Label label1;
        private Label Date_text;
        private Label OF_Text;
        private Label OP_Text;
        private Label Article_Text;
        private Label CDC_Text;
        private TextBox Date_textBox;
        private TextBox OF_textBox;
        private TextBox OP_textBox;
        private TextBox Article_textBox;
        private TextBox CDC_textBox;
        private Label label2;
        private DataGridView Piece_dataGridView;
        private Button Valider_Piece;
        private Label Operateur_name_text;
        private Label User_Text;
        private Label Machine_Label;
        private TextBox Machine_Texbox;
    }
}